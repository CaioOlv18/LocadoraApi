namespace LocadoraApi.Models;

public class Carro : Veiculo, IRastreavel
{
    public int NumeroPortas { get; set; }

    public override decimal CalcularDiaria()
    {
        return 150.00m;
    }

    public override string Descricao()
    {
        return $"Carro: {Modelo}, placa {Placa}, {NumeroPortas} portas";
    }

    public string Rastrear()
    {
        return $"O carro com a placa {Placa} foi localizado.";
    }
}
