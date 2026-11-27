using LocadoraApi.Models;

namespace LocadoraApi.Repositories;

public interface IVeiculoRepository
{
    IEnumerable<Veiculo> ObterTodos();

    Veiculo? ObterPorId(int id);

    void Adicionar(Veiculo veiculo);
}
