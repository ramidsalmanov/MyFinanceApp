using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyFinance.Domain.Models;
using MyFinance.Domain.Models.Budget;
using MyFinance.Domain.Models.Transactions;
using MyFinance.Persistence.Configurations.EntityConfigurations;

namespace MyFinance.Persistence;

internal sealed class AppDbContext : DbContext, IDbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<BudgetItem> BudgetItems { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Category> Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity("AppUserCategory").HasData(
            new
            {
                UsersId = AppUserEntityConfiguration.APP_USER_ID,
                CategoriesId = 1L
            },
            new
            {
                UsersId = AppUserEntityConfiguration.APP_USER_ID,
                CategoriesId = 2L
            });
    }

    public void ApplyPatch<TEntity>(TEntity entity, Dictionary<string, object> properties) where TEntity : class
    {
        throw new System.NotImplementedException();
    }
}

internal class DesignTimeDbContextProvider : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(configuration.GetConnectionString("dev"));


        return new AppDbContext(builder.Options);
    }
}