using Dapper;
using InternetBanking.API.Entidades;
using InternetBanking.API.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.API.Dados.Repositorios
{
    public sealed class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Cliente cliente)
        {
            await _context.Clientes
                .Where(x => x.Id.Equals(cliente.Id))
                .ExecuteUpdateAsync(x => x.SetProperty(c => c.Saldo, cliente.Saldo));
        }

        public async Task<Cliente?> Extrato(int id)
        {
            return await _context.Clientes
               .Include(x => x.Transacoes)
               .AsNoTracking()
               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente?> Obter(int id)
        {
            var query = "SELECT * FROM \"Clientes\" WHERE \"Id\" = @Id";
            var param = new { Id = id };

            var cnn = _context.Database.GetDbConnection();
            return await cnn.QuerySingleOrDefaultAsync<Cliente>(query, param);
        }
    }
}
