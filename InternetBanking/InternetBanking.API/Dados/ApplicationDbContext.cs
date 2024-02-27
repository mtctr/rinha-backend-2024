using InternetBanking.API.Entidades;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace InternetBanking.API.Dados
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transacao>()
                .Property(t => t.RealizadaEm)
                .HasColumnType(NpgsqlDbType.Timestamp.ToString());

            modelBuilder.Entity<Cliente>()
                .Property(c => c.ConcurrencyToken)
                .IsRowVersion();

            modelBuilder.Entity<Cliente>().HasData(new Cliente(1, "Fulano 1", 100000, 0));
            modelBuilder.Entity<Cliente>().HasData(new Cliente(2, "Fulano 2", 80000, 0));
            modelBuilder.Entity<Cliente>().HasData(new Cliente(3, "Fulano 3", 1000000, 0));
            modelBuilder.Entity<Cliente>().HasData(new Cliente(4, "Fulano 4", 10000000, 0));
            modelBuilder.Entity<Cliente>().HasData(new Cliente(5, "Fulano 5", 500000, 0));

        }
    }
}
