using System.Text.Json.Serialization;

namespace InternetBanking.API.DTOs.Results;

public sealed class SaldoDTO {
    public SaldoDTO(int total, string dataExtrato, int limite)
    {
        Total = total;
        DataExtrato = dataExtrato;
        Limite = limite;
    }

    [JsonPropertyName("total")]
    public int Total { get; init; }
    
    [JsonPropertyName("data_extrato")]
    public string DataExtrato { get; init; }
    
    [JsonPropertyName("limite")]
    public int Limite { get; init; }

}
