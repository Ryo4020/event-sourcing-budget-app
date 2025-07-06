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
        services.AddTransient<IRegisterCategoryUsecase, RegisterCategoryUsecase>();

        return services;
    }

    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddTransient<ICategoryDataAccess, CategoryDataAccess>();
        services.AddTransient<IMoneyOperationDataAccess, MoneyOperationDataAccess>();

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

        services.AddScoped<DynamoDbContext>();
        services.AddScoped<MoneyOperationEventTableDao>();

        return services;
    }

    public static IServiceCollection AddPublishers(this IServiceCollection services)
    {
        services.AddTransient<IEventPublisher<CategoryRegistered>, CategoryRegisteredPublisher>();
        services.AddTransient<IEventPublisher<CategoryRenamed>, CategoryRenamedPublisher>();
        services.AddTransient<IEventPublisher<CategoryDeleted>, CategoryDeletedPublisher>();
        services.AddTransient<IEventPublisher<MoneyOperationRegistered>, MoneyOperationRegisteredPublisher>();
        services.AddTransient<IEventPublisher<MoneyOperationEdited>, MoneyOperationEditedPublisher>();
        services.AddTransient<IEventPublisher<MoneyOperationDeleted>, MoneyOperationDeletedPublisher>();

        return services;
    }

    public static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        // Register all event subscribers
        services.AddTransient<IEventSubscriber<CategoryRegistered>, CategoryDataAccess>();
        services.AddTransient<IEventSubscriber<CategoryRenamed>, CategoryDataAccess>();
        services.AddTransient<IEventSubscriber<CategoryDeleted>, CategoryDataAccess>();
        services.AddTransient<IEventSubscriber<MoneyOperationRegistered>, MoneyOperationDataAccess>();
        services.AddTransient<IEventSubscriber<MoneyOperationEdited>, MoneyOperationDataAccess>();
        services.AddTransient<IEventSubscriber<MoneyOperationDeleted>, MoneyOperationDataAccess>();

        return services;
    }
}