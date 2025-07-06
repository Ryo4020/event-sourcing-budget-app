namespace BudgetAppProject.Extensions;

using Amazon.DynamoDBv2;
using BudgetAppProject.Application.Usecase.RegisterCategory;
using BudgetAppProject.DomainModel.Aggregate.Category.Event;
using BudgetAppProject.DomainModel.Aggregate.MoneyOperation.Event;
using BudgetAppProject.DomainService;
using BudgetAppProject.DomainService.DataAccess;
using BudgetAppProject.Infrastructure.DataAccess;
using BudgetAppProject.Infrastructure.DataAccess.AWS;
using BudgetAppProject.Infrastructure.Publisher.Category;

public static class DIExtension
{
    public static IServiceCollection AddUsecases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterCategoryUsecase, RegisterCategoryUsecase>();

        return services;
    }

    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<ICategoryDataAccess, CategoryDataAccess>();
        services.AddScoped<IMoneyOperationDataAccess, MoneyOperationDataAccess>();

        return services;
    }

    public static IServiceCollection AddAwsContexts(this IServiceCollection services)
    {
        services.AddSingleton(_ => {
            var config = new AmazonDynamoDBConfig
            {
                MaxErrorRetry = 2
            };
            return new AmazonDynamoDBClient(config);
        });

        services.AddSingleton<DynamoDbContext>();
        services.AddScoped<MoneyOperationEventTableDao>();
        services.AddScoped<CategoryStateTableDao>();

        return services;
    }

    public static IServiceCollection AddPublishers(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher<CategoryRegistered>, CategoryRegisteredPublisher>();
        services.AddScoped<IEventPublisher<CategoryRenamed>, CategoryRenamedPublisher>();
        services.AddScoped<IEventPublisher<CategoryDeleted>, CategoryDeletedPublisher>();
        services.AddScoped<IEventPublisher<MoneyOperationRegistered>, MoneyOperationRegisteredPublisher>();
        services.AddScoped<IEventPublisher<MoneyOperationEdited>, MoneyOperationEditedPublisher>();
        services.AddScoped<IEventPublisher<MoneyOperationDeleted>, MoneyOperationDeletedPublisher>();

        return services;
    }

    public static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        // Register all event subscribers
        services.AddScoped<IEventSubscriber<CategoryRegistered>, CategoryDataAccess>();
        services.AddScoped<IEventSubscriber<CategoryRenamed>, CategoryDataAccess>();
        services.AddScoped<IEventSubscriber<CategoryDeleted>, CategoryDataAccess>();
        services.AddScoped<IEventSubscriber<MoneyOperationRegistered>, MoneyOperationDataAccess>();
        services.AddScoped<IEventSubscriber<MoneyOperationEdited>, MoneyOperationDataAccess>();
        services.AddScoped<IEventSubscriber<MoneyOperationDeleted>, MoneyOperationDataAccess>();

        return services;
    }
}