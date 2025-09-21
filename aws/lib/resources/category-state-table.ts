import * as cdk from 'aws-cdk-lib'
import { Construct } from 'constructs'
import * as dynamodb from 'aws-cdk-lib/aws-dynamodb'

import { formatServiceName } from '../../util/format-service-name';

export const generateCategoryStateTable = (scope: Construct) : dynamodb.TableV2 => {
    const table = new dynamodb.TableV2(scope, 'CategoryStateTable', {
        tableName: formatServiceName('CategoryStateTable'),
        partitionKey: { name: 'category_id', type: dynamodb.AttributeType.STRING },
        globalSecondaryIndexes: [
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