using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

builder.Services.AddStorage<LocalStorage>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
