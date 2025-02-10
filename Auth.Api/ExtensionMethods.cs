using Auth.Applicaton.Authentication.JwtToken;
using Auth.Applicaton.Interfaces;
using Auth.Applicaton.User.Repository;
using Auth.Applicaton.Interfaces;
using Auth.Contracts.DTO;
using Auth.Contracts.Validators;
using Auth.DataAccess;
using Auth.DataAccess.Data;
using Auth.DataAccess.Interfaces;
using Auth.Infrastructure.DataAccess.Repository;
using Auth.Infrastructure.DataAccess.Repository.Base;
using Auth.Infrastructure.RabbitMQ.Interfaces;
using Auth.Infrastructure.RabbitMQ;
using AuthServiceApplicaton.Authentication.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.API
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
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();

            //Генерация токена
            serviceCollection.AddScoped<IJwtProvider,JwtProvider>();

            //брокер сообщений RabbitMQ
            serviceCollection.AddScoped<IRabbitMqService, RabbitMqService>();
  
        }
        /// <summary>
        /// сервисы FluentValidation в DI
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddFluentValidationAutoValidation(this IServiceCollection serviceCollection)
        {
            //Валидация данных
            serviceCollection.AddScoped<IValidator<RegisterDto>, UserRegistrationValidator>();
            serviceCollection.AddScoped<IValidator<LoginDto>, UserLoginValidator>();    
        }
        /// <summary>
        /// Добавить репозитории в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
        /// <summary>
        /// Добавить DbContext с конфигурациями в DI
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы</param>
        public static void AddDbContextWithConfigurations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDbContextOptionsConfigurator<AuthServiceDbContext>, AuthServiceDbContextConfiguration>();
            serviceCollection.AddDbContext<AuthServiceDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AuthServiceDbContext>>()
                    .Configure((DbContextOptionsBuilder<AuthServiceDbContext>)dbOptions)));
            serviceCollection.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<AuthServiceDbContext>()));
        }

        /// <summary>
        /// Добавляет аутентификацию API с использованием JWT в контейнер зависимостей (DI).
        /// </summary>
        /// <param name="serviceCollection">Сервисы программы.</param>
        /// <param name="JwtOptions">Настройки JWT.</param>
        public static void AddApiAuthentication(this IServiceCollection serviceCollection, IOptions<JwtOptions> JwtOptions)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateLifetime = true,
                        ValidateActor = false,
                        ValidateTokenReplay = false,

                        ValidateAudience = true,
                        ValidAudience = JwtOptions.Value.Audience,

                        ValidateIssuer = true,
                        ValidIssuer = JwtOptions.Value.Issuer,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Value.SecurityKey))
                    };
                }
                );

            serviceCollection.AddAuthorization();
        }
    }
}
