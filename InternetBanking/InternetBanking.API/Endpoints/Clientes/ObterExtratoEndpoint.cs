using Ardalis.ApiEndpoints;
using InternetBanking.API.DTOs.Results;
using InternetBanking.API.Interfaces.Repositorios;
using InternetBanking.API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace InternetBanking.API.Endpoints.Clientes;

public sealed class ObterExtratoEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithResult<ActionResult<ObterExtratoResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ObterExtratoEndpoint(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet("/clientes/{id}/extrato")]
    public override async Task<ActionResult<ObterExtratoResponse>> HandleAsync([FromRoute]int id, CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.Extrato(id);
        if (cliente is null)
            return NotFound(MensagensRetorno.ClienteNaoEncontrado);

        var saldo = new SaldoDTO(cliente.Saldo, DateTime.Now.ToString("s"), cliente.Limite);
        var transacoes = cliente.Transacoes
            .Select(x => new TransacaoDTO(x.Valor, x.Tipo, x.Descricao, x.RealizadaEm.ToString("s")))
            .ToList();

        var response = new ObterExtratoResponse(saldo, transacoes);
        return Ok(response);
    }
}

public sealed class ObterExtratoResponse {
    public ObterExtratoResponse(SaldoDTO saldo, IEnumerable<TransacaoDTO> ultimasTransacoes)
    {
        Saldo = saldo;
        UltimasTransacoes = ultimasTransacoes;
    }

    [JsonPropertyName("saldo")]
    public SaldoDTO Saldo { get; init; }
    
    [JsonPropertyName("ultimas_transacoes")]
    public IEnumerable<TransacaoDTO> UltimasTransacoes { get; init; }
}