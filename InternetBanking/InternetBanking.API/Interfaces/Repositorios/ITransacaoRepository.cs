using InternetBanking.API.Entidades;

namespace InternetBanking.API.Interfaces.Repositorios
{
    public interface ITransacaoRepository
    {
        Task Adicionar(Transacao transacao);
    }
}
