namespace InternetBanking.API.Entidades
{
    public class Cliente
    {
        public Cliente(int id, string nome, int limite, int saldo)
        {
            Id = id;
            Nome = nome;
            Limite = limite;
            Saldo = saldo;
        }

        public int Id { get; init; }
        public string Nome { get; private set; }
        public int Limite { get; private set; }
        public int Saldo { get; private set; }

        public IEnumerable<Transacao> Transacoes { get; private set; }
    }
}
