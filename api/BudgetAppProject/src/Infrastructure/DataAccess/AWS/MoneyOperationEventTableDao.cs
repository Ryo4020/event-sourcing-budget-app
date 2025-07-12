namespace BudgetAppProject.Infrastructure.DataAccess.AWS;

using System.Collections.Immutable;
using Amazon.DynamoDBv2.Model;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainModel.Utils;
using BudgetAppProject.Infrastructure.Enum;
using BudgetAppProject.Infrastructure.Utils;

public class MoneyOperationEventTableDao
{
    private readonly DynamoDbContext _dynamoDbContext;

    private readonly string _eventTableName;

    public MoneyOperationEventTableDao(DynamoDbContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
        _eventTableName = LoadTableNameFromEnvironmentVariable();
    }

    public async Task<ImmutableArray<MoneyOperationEvent>> GetEventsByIdAsync(string moneyOperationId)
    {
        var request = new QueryRequest
        {
            TableName = _eventTableName,
            IndexName = "event-target-index",
            KeyConditionExpression = "event_target_id = :id",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":id", new AttributeValue { S = moneyOperationId } }
            }
        };

        var response = await _dynamoDbContext.DoQueryAsync(request);

        var events = response.Items
            .Select(MapFromDynamoDbItemTonEvent)
            .ToImmutableArray();

        return events;
    }

    public async Task<ImmutableArray<MoneyOperationEvent>> GetEventsByUserIdAsync(string userId)
    {
        var request = new QueryRequest
        {
            TableName = _eventTableName,
            IndexName = "user-id-index",
            KeyConditionExpression = "user_id = :userId",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":userId", new AttributeValue { S = userId } }
            }
        };

        var response = await _dynamoDbContext.DoQueryAsync(request);

        var events = response.Items
            .Select(MapFromDynamoDbItemTonEvent)
            .ToImmutableArray();

        return events;
    }

    public async Task<ImmutableArray<MoneyOperationEvent>> GetEventsByCategoryIdAsync(string categoryId, string? userId)
    {
        var keyConditionExpression = "event_target_category_id = :categoryId";
        var expressionAttributeValues = new Dictionary<string, AttributeValue>
        {
            { ":categoryId", new AttributeValue { S = categoryId } }
        };

        if (!string.IsNullOrEmpty(userId))
        {
            keyConditionExpression += " AND user_id = :userId";
            expressionAttributeValues.Add(":userId", new AttributeValue { S = userId });
        }

        var request = new QueryRequest
        {
            TableName = _eventTableName,
            IndexName = "category-id-index",
            KeyConditionExpression = keyConditionExpression,
            ExpressionAttributeValues = expressionAttributeValues
        };

        var response = await _dynamoDbContext.DoQueryAsync(request);

        var events = response.Items
            .Select(MapFromDynamoDbItemTonEvent)
            .ToImmutableArray();

        return events;
    }

    public async Task AddRegisteredEventAsync(MoneyOperationRegistered registeredEvent)
    {
        var request = new PutItemRequest
        {
            TableName = _eventTableName,
            Item = new Dictionary<string, AttributeValue>()
            {
                { "event_id", new AttributeValue { S = registeredEvent.EventId } },
                { "event_at", new AttributeValue { S = registeredEvent.EventAt.ToString("o") } },
                { "user_id", new AttributeValue { S = registeredEvent.EventTarget.UserId } },
                { "event_type", new AttributeValue { S = MoneyOperationEventType.Registered.ToString() } },
                { "event_target_id", new AttributeValue { S = registeredEvent.EventTarget.Id } },
                { "event_target_title", new AttributeValue { S = registeredEvent.EventTarget.Title } },
                { "event_target_description", new AttributeValue { S = registeredEvent.EventTarget.Description ?? string.Empty } },
                { "event_target_price", new AttributeValue { N = registeredEvent.EventTarget.Price.ToString() } },
                { "event_target_operation_at", new AttributeValue { S = registeredEvent.EventTarget.OperationAt.ToString("o") } },
                { "event_target_type", new AttributeValue { N = EnumHelper.GetValueEnum(registeredEvent.EventTarget.Type).ToString() } },
                { "event_target_category_id", new AttributeValue { S = registeredEvent.EventTarget.CategoryId } }
            }
        };

        await _dynamoDbContext.PutItemAsync(request);
    }

    public async Task AddEditedEventAsync(MoneyOperationEdited editedEvent)
    {
        var request = new PutItemRequest
        {
            TableName = _eventTableName,
            Item = new Dictionary<string, AttributeValue>()
            {
                { "event_id", new AttributeValue { S = editedEvent.EventId } },
                { "event_at", new AttributeValue { S = editedEvent.EventAt.ToString("o") } },
                { "user_id", new AttributeValue { S = editedEvent.EventTarget.UserId } },
                { "event_type", new AttributeValue { S = MoneyOperationEventType.Edited.ToString() } },
                { "event_target_id", new AttributeValue { S = editedEvent.EventTarget.Id } },
                { "event_target_title", new AttributeValue { S = editedEvent.EventTarget.Title } },
                { "event_target_description", new AttributeValue { S = editedEvent.EventTarget.Description ?? string.Empty } },
                { "event_target_price", new AttributeValue { N = editedEvent.EventTarget.Price.ToString() } },
                { "event_target_operation_at", new AttributeValue { S = editedEvent.EventTarget.OperationAt.ToString("o") } },
                { "event_target_type", new AttributeValue { N = EnumHelper.GetValueEnum(editedEvent.EventTarget.Type).ToString() } },
                { "event_target_category_id", new AttributeValue { S = editedEvent.EventTarget.CategoryId } }
            }
        };

        await _dynamoDbContext.PutItemAsync(request);
    }

    public async Task AddDeletedEventAsync(MoneyOperationDeleted deletedEvent, string userId, string categoryId)
    {
        var request = new PutItemRequest
        {
            TableName = _eventTableName,
            Item = new Dictionary<string, AttributeValue>()
            {
                { "event_id", new AttributeValue { S = deletedEvent.EventId } },
                { "event_at", new AttributeValue { S = deletedEvent.EventAt.ToString("o") } },
                { "user_id", new AttributeValue { S = userId } },
                { "event_type", new AttributeValue { S = MoneyOperationEventType.Deleted.ToString() } },
                { "event_target_id", new AttributeValue { S = deletedEvent.EventTargetId } },
                { "event_target_category_id", new AttributeValue { S = categoryId } },
            }
        };

        await _dynamoDbContext.PutItemAsync(request);
    }

    private static MoneyOperationEvent MapFromDynamoDbItemTonEvent(Dictionary<string, AttributeValue> item)
    {
        var eventTypeStr = DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_type");
        if (!System.Enum.TryParse<MoneyOperationEventType>(eventTypeStr, out var eventType))
        {
            throw new InvalidOperationException($"Invalid event type: {eventTypeStr}");
        }

        switch (eventType)
        {
            case MoneyOperationEventType.Registered:
                return new MoneyOperationRegistered(
                    eventId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_id"),
                    eventAt: DynamoDbAttributeHelper.GetRequiredDatetimeAttributeValue(item, "event_at"),
                    moneyOperation: MapFromDynamoDbItemToMoneyOperation(item)
                );

            case MoneyOperationEventType.Edited:
                return new MoneyOperationEdited(
                    eventId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_id"),
                    eventAt: DynamoDbAttributeHelper.GetRequiredDatetimeAttributeValue(item, "event_at"),
                    moneyOperation: MapFromDynamoDbItemToMoneyOperation(item)
                );

            case MoneyOperationEventType.Deleted:
                return new MoneyOperationDeleted(
                    eventId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_id"),
                    eventAt: DynamoDbAttributeHelper.GetRequiredDatetimeAttributeValue(item, "event_at"),
                    moneyOperationId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_target_id")
                );

            default:
                throw new InvalidOperationException($"Unknown event type: {eventType}");
        }
    }

    private static MoneyOperation MapFromDynamoDbItemToMoneyOperation(Dictionary<string, AttributeValue> item)
    {
        return new MoneyOperation(
            id: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_target_id"),
            title: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_target_title"),
            description: DynamoDbAttributeHelper.GetNullableStringAttributeValue(item, "event_target_description"),
            price: (uint)DynamoDbAttributeHelper.GetRequiredIntAttributeValue(item, "event_target_price"),
            operationAt: DynamoDbAttributeHelper.GetRequiredDatetimeAttributeValue(item, "event_target_operation_at"),
            type: EnumHelper.ParseEnumFromInt<MoneyOperationType>(
                DynamoDbAttributeHelper.GetRequiredIntAttributeValue(item, "event_target_type")
            ),
            userId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_target_user_id"),
            categoryId: DynamoDbAttributeHelper.GetRequiredStringAttributeValue(item, "event_target_category_id")
        );
    }

    private static string LoadTableNameFromEnvironmentVariable()
    {
        string? eventTableName = Environment.GetEnvironmentVariable("MONEY_OPERATION_EVENT_TABLE_NAME");
        if (string.IsNullOrEmpty(eventTableName))
        {
            throw new InvalidOperationException("Environment variable 'MONEY_OPERATION_EVENT_TABLE_NAME' is not set.");
        }

        return eventTableName;
    }
}