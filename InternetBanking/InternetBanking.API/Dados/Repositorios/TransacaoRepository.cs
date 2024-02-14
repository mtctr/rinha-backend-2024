using InternetBanking.API.Entidades;
using InternetBanking.API.Interfaces.Repositorios;

namespace InternetBanking.API.Dados.Repositorios
{
    public sealed class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public TransacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }
    }
}
