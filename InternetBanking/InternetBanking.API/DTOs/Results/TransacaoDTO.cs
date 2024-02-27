using System.Text.Json.Serialization;

namespace InternetBanking.API.DTOs.Results;

public sealed class TransacaoDTO
{
    public TransacaoDTO(int valor, char tipo, string descricao, string realizadaEm)
    {
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        RealizadaEm = realizadaEm;
    }

    [JsonPropertyName("valor")]
    public int Valor { get; init; }
    
    [JsonPropertyName("tipo")]
    public char Tipo { get; init; }
    
    [JsonPropertyName("descricao")]
    public string Descricao { get; init; }
    
    [JsonPropertyName("realizado_em")]
    public string RealizadaEm { get; init; }

}
