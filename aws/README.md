# AWS CDK project
This is CDK Typescript project to deploy AWS resources for **"event-sourcing-budget-app"**

## How to deploy
```
export AWS_PROFILE={aws_profile_name}
export STAGE={ DEV / STG / PR }

npm install

npx cdk deploy EventSourcingBudgetAppTableStack
```

## Resources
Here are stacks and resources to be deployed by this cdk.

- TableStack
    - DynamoDB tables
- AuthStack
    - Cognito user pool