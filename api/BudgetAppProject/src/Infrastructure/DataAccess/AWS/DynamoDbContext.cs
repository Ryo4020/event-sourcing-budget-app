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

    public async Task<ScanResponse> DoScanAsync(ScanRequest request)
    {
        try
        {
            return await _dynamoDbClient.ScanAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to scan DynamoDB", ex);
        }
    }

    public async Task<GetItemResponse> GetItemAsync(GetItemRequest request)
    {
        try
        {
            return await _dynamoDbClient.GetItemAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to get item from DynamoDB", ex);
        }
    }

    public async Task<PutItemResponse> PutItemAsync(PutItemRequest request)
    {
        try
        {
            return await _dynamoDbClient.PutItemAsync(request);
        }
        catch (ConditionalCheckFailedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to put item to DynamoDB", ex);
        }
    }

    public async Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request)
    {
        try
        {
            return await _dynamoDbClient.UpdateItemAsync(request);
        }
        catch (ConditionalCheckFailedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update item in DynamoDB", ex);
        }
    }

    public async Task<DeleteItemResponse> DeleteItemAsync(DeleteItemRequest request)
    {
        try
        {
            return await _dynamoDbClient.DeleteItemAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to delete item from DynamoDB", ex);
        }
    }
}