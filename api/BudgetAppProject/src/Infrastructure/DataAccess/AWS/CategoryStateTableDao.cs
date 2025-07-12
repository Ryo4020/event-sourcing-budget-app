namespace BudgetAppProject.Infrastructure.DataAccess.AWS;

using System.Collections.Immutable;
using Amazon.DynamoDBv2.Model;
using BudgetAppProject.DomainModel.Aggregate.Category;
using BudgetAppProject.Infrastructure.Utils;

public class CategoryStateTableDao
{
    private readonly DynamoDbContext _dynamoDbContext;

    private readonly string _stateTableName;

    public CategoryStateTableDao(DynamoDbContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
        _stateTableName = LoadTableNameFromEnvironmentVariable();
    }

    public async Task<Category> GetByIdAsync(string categoryId)
    {
        var request = new GetItemRequest
        {
            TableName = _stateTableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "category_id", new AttributeValue { S = categoryId } }
            }
        };

        var response = await _dynamoDbContext.GetItemAsync(request);
        if (response.Item == null || !response.IsItemSet)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }

        return MapFromDynamoDbItemToCategory(response.Item);
    }

    public async Task<Category?> GetByNameAsync(string name, bool isDefault, string? userId)
    {
        var filterExpression = "category_name = :category_name AND is_default = :isDefault";
        var expressionAttributeValues = new Dictionary<string, AttributeValue>
        {
            { ":category_name", new AttributeValue { S = name } },
            { ":isDefault", new AttributeValue { BOOL = isDefault } }
        };

        if (!string.IsNullOrEmpty(userId))
        {
            filterExpression += " AND user_id = :userId";
            expressionAttributeValues.Add(":userId", new AttributeValue { S = userId });
        }

        var request = new ScanRequest
        {
            TableName = _stateTableName,
            FilterExpression = filterExpression,
            ExpressionAttributeValues = expressionAttributeValues,
            Limit = 1
        };

        var response = await _dynamoDbContext.DoScanAsync(request);
        if (response.Items.Count == 0)
        {
            return null;
        } else if (response.Items.Count > 1)
        {
            throw new InvalidOperationException($"Multiple categories found with name '{name}'.");
        }

        return MapFromDynamoDbItemToCategory(response.Items[0]);
    }

    public async Task<ImmutableArray<Category>> GetAllByUserIdAsync(string userId)
    {
        var request = new QueryRequest
        {
            TableName = _stateTableName,
            IndexName = "user-id-index",
            KeyConditionExpression = "user_id = :userId",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":userId", new AttributeValue { S = userId } }
            }
        };

        var response = await _dynamoDbContext.DoQueryAsync(request);
        var categories = response.Items
            .Select(MapFromDynamoDbItemToCategory)
            .ToImmutableArray();

        return categories;
    }

    public async Task AddStateAsync(Category category)
    {
        var item = new Dictionary<string, AttributeValue>
        {
            { "category_id", new AttributeValue { S = category.Id } },
            { "category_name", new AttributeValue { S = category.Name } },
            { "is_default", new AttributeValue { BOOL = category.IsDefault } }
        };

        if (category is EditableCategory editableCategory)
        {
            item.Add("user_id", new AttributeValue { S = editableCategory.UserId });
        }

        var request = new PutItemRequest
        {
            TableName = _stateTableName,
            Item = item,
            ConditionExpression = "attribute_not_exists(category_id)",
        };

        await _dynamoDbContext.PutItemAsync(request);
    }

    public async Task RenameAsync(string categoryId, string newName)
    {
        var request = new UpdateItemRequest
        {
            TableName = _stateTableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "category_id", new AttributeValue { S = categoryId } }
            },
            ConditionExpression = "attribute_exists(category_id)",
            UpdateExpression = "SET category_name = :newName",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":newName", new AttributeValue { S = newName } }
            }
        };

        await _dynamoDbContext.UpdateItemAsync(request);
    }

    public async Task DeleteAsync(string categoryId)
    {
        var request = new DeleteItemRequest
        {
            TableName = _stateTableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "category_id", new AttributeValue { S = categoryId } }
            }
        };

        await _dynamoDbContext.DeleteItemAsync(request);
    }

    private static Category MapFromDynamoDbItemToCategory(Dictionary<string, AttributeValue> item)
    {
        var isDefault = DynamoDbAttributeHelper.GetRequiredBooleanAttributeValue(item, "is_default");
        if (isDefault)
        {
            return new DefaultCategory(
                id: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "category_id"),
                name: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "category_name")
            );
        }

        return new EditableCategory(
            id: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "category_id"),
            name: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "category_name"),
            userId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "user_id")
        );
    }

    private static string LoadTableNameFromEnvironmentVariable()
    {
        string? eventTableName = Environment.GetEnvironmentVariable("CATEGORY_STATE_TABLE_NAME");
        if (string.IsNullOrEmpty(eventTableName))
        {
            throw new InvalidOperationException("Environment variable 'CATEGORY_STATE_TABLE_NAME' is not set.");
        }

        return eventTableName;
    }
}