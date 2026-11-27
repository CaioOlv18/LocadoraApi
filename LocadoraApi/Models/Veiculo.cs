namespace LocadoraApi.Models;

public abstract class Veiculo
{
    public int Id { get; set; }

    public string Placa { get; set; } = string.Empty;

    public string Modelo { get; set; } = string.Empty;

    public int AnoFabricacao { get; set; }

    public DateTime? DataLocacao { get; set; }

    public const int PrazoLocacaoDias = 3;

    // Cada tipo de veículo vai informar sua própria diária.
    public abstract decimal CalcularDiaria();

    // Descrição padrão, que pode ser alterada nas classes filhas.
    public virtual string Descricao()
    {
        return $"{Modelo} - Placa: {Placa} - Ano: {AnoFabricacao}";
    }
}
