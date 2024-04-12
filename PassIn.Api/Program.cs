using PassIn.Application.Configurations;
using PassIn.Application.Data.Mapping;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

builder.Services.AddDbContexts(builder.Configuration)
                .AddAutoMapper(typeof(DomainToDtoProfile), typeof(DtoToDomainProfile))
                .AddDependencyInjections()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
