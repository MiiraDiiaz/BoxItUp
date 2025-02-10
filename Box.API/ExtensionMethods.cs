using Box.Applicaton.BackgroundServices;
using Box.Applicaton.Handler;
using Box.Applicaton.Interface;
using Box.Applicaton.JwtToken;
using Box.Applicaton.Service;
using Box.Contracts.DTO;
using Box.Contracts.Validators;
using Box.Infrastructure.DataAccess;
using Box.Infrastructure.DataAccess.Configurations;
using Box.Infrastructure.DataAccess.Interfaces;
using Box.Infrastructure.DataAccess.Repository;
using Box.Infrastructure.DataAccess.Repository.Base;
using Box.Infrastructure.RabbitMq;
using Box.Infrastructure.RabbitMq.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Box.API
{
    /// <summary>
    /// Методы расширения класса Program
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Добавить сервисы в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            // Сервисы сущностей
            serviceCollection.AddScoped<IBoxService, BoxService>();
            serviceCollection.AddScoped<IItemBoxService, ItemBoxService>();

            serviceCollection.AddHttpContextAccessor();

            //Авторизация
            serviceCollection.AddScoped<IAuthorizationHandler, TokenAuthorizationHandler>();

            //Генерация токена
            serviceCollection.AddSingleton<IJwtProvider,JwtProvider>();

            //брокер сообщений RabbitMQ
            serviceCollection.AddSingleton<IRabbitMqService, RabbitMqService>();
            // Регистрация сервиса в фоновом режиме
            serviceCollection.AddSingleton<RabbitMqListenerService>();
            serviceCollection.AddHostedService<RabbitMqListenerService>();   
        }
        /// <summary>
        /// сервисы FluentValidation в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddFluentValidationAutoValidation(this IServiceCollection serviceCollection)
        {
            //Валидация данных
            serviceCollection.AddScoped<IValidator<ShortBoxDto>, BoxValidator>();
            serviceCollection.AddScoped<IValidator<ShortItemBoxDto>, ItemBoxValidator>();
        }
        /// <summary>
        /// Добавить репозитории в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IBoxRepository, BoxRepository>();
            serviceCollection.AddScoped<IItemBoxRepository, ItemBoxRepository>();
        }
        /// <summary>
        /// Добавить DbContext с конфигурациями в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddDbContextWithConfigurations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDbContextOptionsConfigurator<BoxServiceDbContext>, BoxServiceDbContextConfiguration>();
            serviceCollection.AddDbContext<BoxServiceDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BoxServiceDbContext>>()
                    .Configure((DbContextOptionsBuilder<BoxServiceDbContext>)dbOptions)));
            serviceCollection.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BoxServiceDbContext>()));
        }

        /// <summary>
        /// Добавить политику авторизации
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddApiAuthentication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnly", policy => policy.Requirements.Add(new UserOnlyRequirement()));
            });
        }
    }
}
