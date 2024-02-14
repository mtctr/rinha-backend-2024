 namespace InternetBanking.API.Entidades;

public sealed class Cliente
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

    public bool PodeRealizarTransacao(Transacao transacao)
    {
        if (transacao.Valor < 0) return false;
        if (transacao.EhCredito) return true;       
        if (transacao.EhDebito)
        {
            if (Limite >= Math.Abs(Saldo - transacao.Valor)) return true;
            else return false;
        }
        return false;
    }

    public void RealizarTransacao(Transacao transacao)
    {        
        if (transacao.EhCredito)
            Saldo = Saldo + transacao.Valor;
        else if (transacao.EhDebito)
            Saldo = Saldo - transacao.Valor;        
    }
}
