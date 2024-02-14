using InternetBanking.API.Entidades;

namespace InternetBanking.API.Interfaces.Repositorios
{
    public interface ITransacaoRepository
    {
        void Adicionar(Transacao transacao);
    }
}
