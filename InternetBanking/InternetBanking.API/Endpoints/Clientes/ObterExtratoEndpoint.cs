using Ardalis.ApiEndpoints;
using InternetBanking.API.DTOs.Results;
using InternetBanking.API.Interfaces.Repositorios;
using InternetBanking.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.API.Endpoints.Clientes;

public class ObterExtratoEndpoint : EndpointBaseAsync
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
        var cliente = _clienteRepository.ObterExtrato(id);
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

public record ObterExtratoResponse(SaldoDTO Saldo, IEnumerable<TransacaoDTO> UltimasTransacoes);