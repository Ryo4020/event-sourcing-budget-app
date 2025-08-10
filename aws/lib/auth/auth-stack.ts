import { Construct } from "constructs";
import * as cdk from 'aws-cdk-lib';

import { generateUserPool } from "./resources/user-pool";

export class AuthStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    generateUserPool(this);
  }
}