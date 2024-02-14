using InternetBanking.API.Entidades;

namespace InternetBanking.API.Interfaces.Repositorios
{
    public interface IClienteRepository
    {
        public Cliente? Obter(int id);
        public void Atualizar(Cliente cliente);
    }
}
