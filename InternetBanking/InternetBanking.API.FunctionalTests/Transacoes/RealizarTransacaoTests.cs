using FluentAssertions;
using InternetBanking.API.Endpoints.Clientes;
using InternetBanking.API.FunctionalTests.Abstractions;
using InternetBanking.API.Utils;
using System.Net;
using System.Net.Http.Json;

namespace InternetBanking.API.FunctionalTests.Transacoes
{
    public class RealizarTransacaoTests : BaseFunctionalTest
    {
        public RealizarTransacaoTests(FunctionalTestsWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Deve_retornar_OK_com_limite_e_saldo_novos_ao_fazer_uma_transacao()
        {
            //Arrange
            var idCliente = 1;
            var transacao = new TransacaoBody(1000, 'c', "descricao");
            
            //Act
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync<TransacaoBody>($"clientes/{idCliente}/transacoes", transacao);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<RealizarTransacaoResult>();
            content.Limite.ToString().Should().NotBeEmpty();
            content.Saldo.ToString().Should().NotBeEmpty();

        }

        [Fact]
        public async Task Deve_retornar_BadRequest_com_mensagem_erro_caso_tipo_seja_diferente_de_c_ou_d()
        {
            //Arrange
            var idCliente = 1;
            var transacao = new TransacaoBody(1000, 'e', "descricao");

            //Act
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync<TransacaoBody>($"clientes/{idCliente}/transacoes", transacao);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be(MensagensRetorno.TipoInvalido);

        }

        [Fact]
        public async Task Deve_retornar_NotFound_quando_cliente_nao_existir()
        {
            //Arrange
            var idClienteNaoExistente = 6;
            var transacao = new TransacaoBody(1000, 'c', "descricao");

            //Act
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync<TransacaoBody>($"clientes/{idClienteNaoExistente}/transacoes", transacao);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be(MensagensRetorno.ClienteNaoEncontrado);
        }

        [Fact]
        public async Task Deve_retornar_422_caso_debito_deixe_o_saldo_inconsistente()
        {
            //Arrange
            var idClienteNaoExistente = 1; //limite = 100000            
            var transacao = new TransacaoBody(1000001, 'd', "descricao");

            //Act
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync<TransacaoBody>($"clientes/{idClienteNaoExistente}/transacoes", transacao);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be(MensagensRetorno.SaldoInconsistente);
        }
    }
}
