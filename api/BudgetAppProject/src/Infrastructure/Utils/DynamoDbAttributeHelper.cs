namespace BudgetAppProject.Infrastructure.Utils;

using Amazon.DynamoDBv2.Model;

public static class DynamoDbAttributeHelper
{
    public static string GetRequiredStringAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value.S))
        {
            return value.S;
        }

        throw new KeyNotFoundException($"Attribute '{key}' not found or is null in the DynamoDB item.");
    }

    public static string? GetNullableStringAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value.S))
        {
            return value.S;
        }

        return null;
    }

    public static int GetRequiredIntAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && value.N != null && int.TryParse(value.N, out var intValue))
        {
            return intValue;
        }

        throw new KeyNotFoundException($"Attribute '{key}' not found or is null in the DynamoDB item.");
    }

    public static int? GetNullableIntAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && value.N != null && int.TryParse(value.N, out var intValue))
        {
            return intValue;
        }

        return null;
    }

    public static DateTimeOffset GetRequiredDatetimeAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value.S) && DateTimeOffset.TryParse(value.S, out var dateTimeValue))
        {
            return dateTimeValue;
        }

        throw new KeyNotFoundException($"Attribute '{key}' not found or is null in the DynamoDB item.");
    }

    public static DateTimeOffset? GetNullableDatetimeAttributeValue(Dictionary<string, AttributeValue> item, string key)
    {
        if (item.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value.S) && DateTimeOffset.TryParse(value.S, out var dateTimeValue))
        {
            return dateTimeValue;
        }

        return null;
    }
}