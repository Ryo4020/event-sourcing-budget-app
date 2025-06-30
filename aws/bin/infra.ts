#!/usr/bin/env node
import * as cdk from 'aws-cdk-lib';

import { TableStack } from '../lib/table-stack';
import { formatServiceName } from '../util/format-service-name';

const env = {
  account: process.env.CDK_ACCOUNT,
  region: process.env.CDK_REGION
}

const app = new cdk.App();
new TableStack(app, formatServiceName('TableStack'), { env });