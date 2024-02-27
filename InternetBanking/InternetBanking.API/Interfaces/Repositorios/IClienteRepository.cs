using InternetBanking.API.Entidades;

namespace InternetBanking.API.Interfaces.Repositorios
{
    public interface IClienteRepository
    {
        public Task<Cliente?> Obter(int id);
        public Task<Cliente?> Extrato(int id);
        public Task Atualizar(Cliente cliente);
    }
}
