import { getRequiredEnvVar } from "./environment-variables";

export const formatServiceName = (name: string, lowercase?: boolean): string => {
    const servicePrefix = lowercase ? 'event-sourcing-budget-app-' : 'EventSourcingBudgetApp';

    const stageVariable = getRequiredEnvVar('STAGE');
    const stage = lowercase ? stageVariable.toLowerCase() : stageVariable;

    return servicePrefix + name + '-' + stage;
}