namespace InternetBanking.API.DTOs.Results
{
    public record TransacaoDTO(
        int Valor,
        char Tipo,
        string Descricao,
        string RealizadaEm
        );
}
