import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';

import { generateMoneyOperationEventTable } from './resources/money-operation-event-table';
import { generateCategoryStateTable } from './resources/category-state-table';

export class TableStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    generateMoneyOperationEventTable(this);
    generateCategoryStateTable(this);
  }
}
