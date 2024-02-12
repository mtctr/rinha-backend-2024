using FluentAssertions;
using InternetBanking.API.Endpoints.Clientes;
using InternetBanking.API.FunctionalTests.Abstractions;
using InternetBanking.API.Utils;
using System.Net;
using System.Net.Http.Json;

namespace InternetBanking.API.FunctionalTests.Extratos
{
    public class ObterExtratoTests : BaseFunctionalTest
    {
        public ObterExtratoTests(FunctionalTestsWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Deve_retornar_Ok_quando_cliente_existir()
        {
            //Arrange
            var idClienteExistente = 1;

            //Act
            HttpResponseMessage response = await HttpClient.GetAsync($"clientes/{idClienteExistente}/extrato");

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadFromJsonAsync<ObterExtratoResponse>();
            content.Saldo.DataExtrato.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Deve_retornar_NotFound_quando_cliente_nao_existir()
        {
            //Arrange
            var idClienteNaoExistente = 6;

            //Act
            HttpResponseMessage response = await HttpClient.GetAsync($"/clientes/{idClienteNaoExistente}/extrato");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be(MensagensRetorno.ClienteNaoEncontrado);
        }
    }
}
