using FluentAssertions;
using InternetBanking.API.Entidades;

namespace InternetBanking.API.UnitTests.Transacoes;

public class RealizarCreditoTests
{
    private readonly Cliente _cliente;
    public RealizarCreditoTests()
    {
        _cliente = new Cliente(1, "Fulano 1", 1000, 0);
    }

    [Fact]
    public void Cliente_pode_realizar_transacao_se_for_credito_e_valor_for_positivo()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'c',
            Valor = 1000
        };

        var podeRealizar = _cliente.PodeRealizarTransacao(transacao);

        podeRealizar.Should().Be(true);
    }

    [Fact]
    public void Cliente_nao_pode_realizar_transacao_se_for_credito_e_valor_for_negativo()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'c',
            Valor = -1000
        };

        var podeRealizar = _cliente.PodeRealizarTransacao(transacao);

        podeRealizar.Should().Be(false);
    }

    [Fact]
    public void Realizar_credito_deve_aumentar_saldo_do_cliente()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'c',
            Valor = 1000
        };

        var saldoAntigo = _cliente.Saldo;
        _cliente.RealizarTransacao(transacao);

        var novoSaldo = saldoAntigo + transacao.Valor;
        _cliente.Saldo.Should().Be(novoSaldo);
    }    
    
}
