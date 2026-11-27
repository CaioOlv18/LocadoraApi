namespace LocadoraApi.Models;

public class Moto : Veiculo
{
    public int Cilindradas { get; set; }

    public override decimal CalcularDiaria()
    {
        return 80.00m;
    }

    public override string Descricao()
    {
        return $"Moto: {Modelo}, placa {Placa}, {Cilindradas} cilindradas";
    }
}
