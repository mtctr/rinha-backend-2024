namespace InternetBanking.API.Entidades
{
    public sealed class Transacao
    {

        public Guid Id { get; init; } = Guid.NewGuid();
        public required int ClienteId { get; init; }
        public required int Valor { get; init; }
        public required char Tipo { get; init; }
        public required string Descricao { get; init; }
        public DateTime RealizadaEm { get; init; } = DateTime.Now;


        public bool EhCredito => Tipo.Equals('c');
        public bool EhDebito => Tipo.Equals('d');

        public Cliente Cliente { get; init; }
    }
}
