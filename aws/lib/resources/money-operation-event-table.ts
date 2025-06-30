import * as cdk from 'aws-cdk-lib'
import { Construct } from 'constructs'
import * as dynamodb from 'aws-cdk-lib/aws-dynamodb'

import { formatServiceName } from '../../util/format-service-name';

export const generateMoneyOperationEventTable = (scope: Construct) : dynamodb.TableV2 => {
    const table = new dynamodb.TableV2(scope, 'MoneyOperationEventTable', {
        tableName: formatServiceName('MoneyOperationEventTable'),
        partitionKey: { name: 'event_id', type: dynamodb.AttributeType.STRING },
        sortKey: { name: 'user_id', type: dynamodb.AttributeType.STRING },
        globalSecondaryIndexes: [
            {
                indexName: 'event-target-index',
                partitionKey: { name: 'event_target_id', type: dynamodb.AttributeType.STRING },
                sortKey: { name: 'user_id', type: dynamodb.AttributeType.STRING },
            },
            {
                indexName: 'user-id-index',
                partitionKey: { name: 'user_id', type: dynamodb.AttributeType.STRING },
            }
        ],
        billing: dynamodb.Billing.onDemand(),
        removalPolicy: cdk.RemovalPolicy.DESTROY,
    });

    return table;
}