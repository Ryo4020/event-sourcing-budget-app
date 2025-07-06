namespace BudgetAppProject.Infrastructure.DataAccess.AWS;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

public class DynamoDbContext
{
    private AmazonDynamoDBClient _dynamoDbClient;

    public DynamoDbContext(AmazonDynamoDBClient dynamoDbClient)
    {
        _dynamoDbClient = dynamoDbClient;
    }

    public async Task<QueryResponse> DoQueryAsync(QueryRequest request)
    {
        try
        {
            return await _dynamoDbClient.QueryAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to query DynamoDB", ex);
        }
    }

    public async Task<PutItemResponse> PutItemAsync(PutItemRequest request)
    {
        try
        {
            return await _dynamoDbClient.PutItemAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to put item to DynamoDB", ex);
        }
    }
}