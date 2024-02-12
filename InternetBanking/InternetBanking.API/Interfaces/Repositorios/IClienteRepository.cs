using InternetBanking.API.Entidades;

namespace InternetBanking.API.Interfaces.Repositorios
{
    public interface IClienteRepository
    {
        public Cliente? ObterExtrato(int id);
    }
}
