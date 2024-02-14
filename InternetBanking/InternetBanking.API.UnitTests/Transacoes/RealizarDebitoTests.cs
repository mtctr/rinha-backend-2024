using FluentAssertions;
using InternetBanking.API.Entidades;

namespace InternetBanking.API.UnitTests.Transacoes;

public class RealizarDebitoTests
{
    private readonly Cliente _cliente;
    public RealizarDebitoTests()
    {
        _cliente = new Cliente(1, "Fulano 1", 1000, 0);
    }

    [Fact]
    public void Cliente_nao_pode_realizar_transacao_se_for_debito_e_valor_for_negativo()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'd',
            Valor = -1000
        };

        var podeRealizar = _cliente.PodeRealizarTransacao(transacao);
        
        podeRealizar.Should().Be(false);
    }


        [Fact]
    public void Cliente_pode_realizar_transacao_se_for_debito_e_saldo_ficar_acima_do_limite()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'd',
            Valor = 1000
        };

        var podeRealizar = _cliente.PodeRealizarTransacao(transacao);

        podeRealizar.Should().Be(true);
    }



    [Fact]
    public void Cliente_nao_pode_realizar_transacao_se_for_debito_e_saldo_ficar_abaixo_do_limite()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'd',
            Valor = 1001
        };

        var podeRealizar = _cliente.PodeRealizarTransacao(transacao);

        podeRealizar.Should().Be(false);
    }

    [Fact]
    public void Realizar_debito_deve_diminuir_saldo_do_cliente()
    {
        var transacao = new Transacao
        {
            ClienteId = _cliente.Id,
            Descricao = "Teste",
            Tipo = 'd',
            Valor = 1000
        };

        var saldoAntigo = _cliente.Saldo;
        _cliente.RealizarTransacao(transacao);

        var novoSaldo = saldoAntigo - transacao.Valor;
        _cliente.Saldo.Should().Be(novoSaldo);
    }

}
