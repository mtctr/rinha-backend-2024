using InternetBanking.API.Dados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace InternetBanking.API.FunctionalTests.Abstractions
{
    public class FunctionalTestsWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("rinha")
            .WithUsername("admin")
            .WithPassword("123")
            .Build();

        override protected void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options
                    .UseNpgsql(_dbContainer.GetConnectionString()));
            });
        }

        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

       public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
