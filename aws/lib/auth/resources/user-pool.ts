import { Construct } from 'constructs';
import * as cdk from 'aws-cdk-lib';
import * as cognito from 'aws-cdk-lib/aws-cognito';

import { formatServiceName } from '../../../util/format-service-name';

export const generateUserPool = (scope: Construct): cognito.UserPool => {
    const userPool = new cognito.UserPool(scope, 'UserPool', {
        userPoolName: formatServiceName('UserPool'),
        selfSignUpEnabled: true,
        signInAliases: { username: true, email: true },
        mfa: cognito.Mfa.OFF,
        passwordPolicy: {
            minLength: 6,
            requireDigits: false,
            requireLowercase: true,
            requireUppercase: false,
            requireSymbols: false,
        },
        accountRecovery: cognito.AccountRecovery.EMAIL_ONLY,
        removalPolicy: cdk.RemovalPolicy.DESTROY
    });

    userPool.addClient('UserPoolClient', {
        userPoolClientName: formatServiceName('UserPoolClient'),
        authFlows: {
            userPassword: true,
            userSrp: true,
        },
    });

    userPool.addDomain('UserPoolDomain', {
        cognitoDomain: { domainPrefix: formatServiceName('', true) },
    });

    return userPool;
}