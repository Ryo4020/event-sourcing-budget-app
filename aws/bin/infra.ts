#!/usr/bin/env node
import * as cdk from 'aws-cdk-lib';

import { AuthStack } from '../lib/auth/auth-stack';
import { TableStack } from '../lib/table/table-stack';
import { formatServiceName } from '../util/format-service-name';

const env = {
  account: process.env.CDK_ACCOUNT,
  region: process.env.CDK_REGION
}

const app = new cdk.App();
new AuthStack(app, formatServiceName('AuthStack'), { env });
new TableStack(app, formatServiceName('TableStack'), { env });