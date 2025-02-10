using Auth.API;
using Auth.Applicaton.Authentication.JwtToken;
using Auth.Infrastructure.MapEntitity;
using Auth.Infrastructure.RabbitMQ;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));

builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

builder.Services.AddDbContextWithConfigurations();

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthentication();

app.MapControllers();

app.Run();

static MapperConfiguration GetMapperConfiguration()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<UserProfile>();
    });
    config.AssertConfigurationIsValid();
    return config;
}
