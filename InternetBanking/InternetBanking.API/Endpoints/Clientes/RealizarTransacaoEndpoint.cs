using Ardalis.ApiEndpoints;
using InternetBanking.API.Entidades;
using InternetBanking.API.Interfaces;
using InternetBanking.API.Interfaces.Repositorios;
using InternetBanking.API.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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
    public override async Task<ActionResult<RealizarTransacaoResult>> HandleAsync([FromRoute] RealizarTransacaoRequest request, CancellationToken cancellationToken = default)
    {

        if (!request.EhValido())
            return BadRequest(MensagensRetorno.TipoInvalido);


        var executionStrategy = _unitOfWork.GetExecutionStrategy();
        var result = await executionStrategy.ExecuteAsync(async Task<ActionResult> () =>
        {
            var cliente = await _clienteRepository.Obter(request.Id);
            if (cliente is null)
                return NotFound(MensagensRetorno.ClienteNaoEncontrado);


            var transacao = new Transacao
            {
                ClienteId = request.Id,
                Descricao = request.Transacao.Descricao,
                Tipo = request.Transacao.Tipo,
                Valor = request.Transacao.Valor,
            };
            if (!cliente.PodeRealizarTransacao(transacao))
                return UnprocessableEntity(MensagensRetorno.SaldoInconsistente);

            using var scope = _unitOfWork.BeginTransaction();
            cliente.RealizarTransacao(transacao);

            await _clienteRepository.Atualizar(cliente);
            await _transacaoRepository.Adicionar(transacao);
            try
            {
                await _unitOfWork.Commit(scope);
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }

            return Ok(new RealizarTransacaoResult(cliente.Limite, cliente.Saldo));
        });

        return result;
    }
}

public sealed class RealizarTransacaoRequest
{
    private IDictionary<char, string> TIPOS_TRANSACAO = new Dictionary<char, string>
        {
            {'c', "Crédito" },
            {'d', "Débito" }
        };
    [FromRoute(Name = "id")] public int Id { get; set; }
    [FromBody] public TransacaoBody Transacao { get; set; }

    public bool EhValido()
    {
        return TIPOS_TRANSACAO.TryGetValue(Transacao.Tipo, out var tipoTransacao)
            && Transacao.Valor > 0
            && Transacao.Descricao.Length >= 1
            && Transacao.Descricao.Length <= 10;
    }
}
public record TransacaoBody(int Valor, char Tipo, string Descricao);
public record RealizarTransacaoResult(int Limite, int Saldo);
