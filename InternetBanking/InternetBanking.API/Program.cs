using System.Net;
using InternetBanking.API.Dados;
using InternetBanking.API.Dados.Repositorios;
using InternetBanking.API.Interfaces;
using InternetBanking.API.Interfaces.Repositorios;
using InternetBanking.API.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.UseNamespaceRouteToken();
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database"), x =>
    {
        x.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null);
    })
    .EnableDetailedErrors());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
    options.SerializerOptions.WriteIndented = true;
});
builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    while (!db.Database.CanConnect())
    {
    }
    try
    {
        await db.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


app.UseExceptionHandler();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/clientes/resetar-saldo", async ([FromServices] ApplicationDbContext context) =>
{
    await context.Clientes.ExecuteUpdateAsync(x => x.SetProperty(c => c.Saldo, 0));
    await context.SaveChangesAsync();
    return HttpStatusCode.OK;
});

app.MapControllers();
app.MapHealthChecks("/healthz");
app.Run();

public partial class Program;