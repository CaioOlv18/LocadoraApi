using LocadoraApi.Models;

namespace LocadoraApi.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    // Os dois veículos iniciais ficam guardados neste dicionário.
    private readonly Dictionary<int, Veiculo> _veiculos = new()
    {
        [1] = new Carro
        {
            Id = 1,
            Placa = "ABC1D23",
            Modelo = "Gol",
            AnoFabricacao = 2020,
            NumeroPortas = 4,
            DataLocacao = new DateTime(2026, 11, 16)
        },

        [2] = new Moto
        {
            Id = 2,
            Placa = "XYZ9K88",
            Modelo = "CG 160",
            AnoFabricacao = 2022,
            Cilindradas = 160,
            DataLocacao = null
        }
    };

    // O próximo cadastro começa com o ID 3.
    private int _proximoId = 3;

    public IEnumerable<Veiculo> ObterTodos()
    {
        return _veiculos.Values;
    }

    public Veiculo? ObterPorId(int id)
    {
        if (_veiculos.ContainsKey(id))
        {
            return _veiculos[id];
        }

        return null;
    }

    public void Adicionar(Veiculo veiculo)
    {
        veiculo.Id = _proximoId;
        _veiculos.Add(_proximoId, veiculo);
        _proximoId++;
    }
}
