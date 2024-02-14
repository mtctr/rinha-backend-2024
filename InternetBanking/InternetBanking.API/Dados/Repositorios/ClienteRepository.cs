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

        public void Atualizar(Cliente cliente)
        {
            _context.Update(cliente);            
        }

        public Cliente? Obter(int id)
        {
            return _context.Clientes
                .Include(x => x.Transacoes)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
