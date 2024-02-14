using Ardalis.ApiEndpoints;
using InternetBanking.API.Entidades;
using InternetBanking.API.Interfaces;
using InternetBanking.API.Interfaces.Repositorios;
using InternetBanking.API.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.API.Endpoints.Clientes;

public sealed class RealizarTransacaoEndpoint : EndpointBaseAsync
    .WithRequest<RealizarTransacaoRequest>
    .WithResult<ActionResult<RealizarTransacaoResult>>

{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizarTransacaoEndpoint(IUnitOfWork unitOfWork, IClienteRepository clienteRepository, ITransacaoRepository transacaoRepository)
    {
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _transacaoRepository = transacaoRepository;
    }

    [HttpPost("/clientes/{id}/transacoes")]
    public override async Task<ActionResult<RealizarTransacaoResult>> HandleAsync([FromRoute]RealizarTransacaoRequest request, CancellationToken cancellationToken = default)
    {

        if (!Transacao.TIPOS_TRANSACAO.TryGetValue(request.Transacao.Tipo, out var tipoTransacao))
            return BadRequest(MensagensRetorno.TipoInvalido);
        
        var cliente = _clienteRepository.Obter(request.Id);
        if (cliente is null)
            return NotFound(MensagensRetorno.ClienteNaoEncontrado);

        var transacao = new Transacao { 
            ClienteId = request.Id, 
            Descricao =  request.Transacao.Descricao,
            Tipo = request.Transacao.Tipo,
            Valor = request.Transacao.Valor,
        };
        if (!cliente.PodeRealizarTransacao(transacao))
            return UnprocessableEntity(MensagensRetorno.SaldoInconsistente);

        cliente.RealizarTransacao(transacao);
       
        using (var scope = _unitOfWork.BeginTransaction())
        {
            _transacaoRepository.Adicionar(transacao);
            _clienteRepository.Atualizar(cliente);
            _unitOfWork.Commit(scope);
        }        
        
        return Ok(new RealizarTransacaoResult(cliente.Limite,cliente.Saldo));
    }
}

public record RealizarTransacaoRequest([FromRoute(Name = "id")] int Id, [FromBody] TransacaoBody Transacao);
public record TransacaoBody(int Valor, char Tipo, string Descricao);
public record RealizarTransacaoResult(int Limite, int Saldo);
