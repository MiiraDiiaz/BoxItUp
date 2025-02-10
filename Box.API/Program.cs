using AutoMapper;
using Box.API;
using Box.Applicaton.JwtToken;
using Box.Infrastructure.MapEntitity;
using Box.Infrastructure.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));

builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

builder.Services.AddDbContextWithConfigurations();

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddApiAuthentication();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static MapperConfiguration GetMapperConfiguration()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<BoxProfile>();
        cfg.AddProfile<ItemBoxProfile>();
    });
    config.AssertConfigurationIsValid();
    return config;
}