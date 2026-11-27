using LocadoraApi.Models;
using LocadoraApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoRepository _repository;

    public VeiculosController(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    // Lista todos os veículos cadastrados.
    [HttpGet]
    public IActionResult Listar()
    {
        var veiculos = _repository.ObterTodos();

        var resultado = veiculos.Select(veiculo => new
        {
            id = veiculo.Id,
            tipo = veiculo.GetType().Name,
            placa = veiculo.Placa,
            modelo = veiculo.Modelo,
            anofabricacao = veiculo.AnoFabricacao,
            descricao = veiculo.Descricao(),
            diaria = veiculo.CalcularDiaria()
        });

        return Ok(resultado);
    }

    // Cadastra um carro novo.
    [HttpPost("carro")]
    public IActionResult CadastrarCarro([FromBody] Carro carro)
    {
        if (string.IsNullOrWhiteSpace(carro.Placa))
        {
            return BadRequest("A placa é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(carro.Modelo))
        {
            return BadRequest("O modelo é obrigatório.");
        }

        int anoAtual = DateTime.Today.Year;

        if (carro.AnoFabricacao < 1900 || carro.AnoFabricacao > anoAtual + 1)
        {
            return BadRequest("O ano de fabricação deve estar entre 1900 e o ano atual + 1.");
        }

        if (carro.NumeroPortas < 2 || carro.NumeroPortas > 5)
        {
            return BadRequest("O número de portas deve estar entre 2 e 5.");
        }

        _repository.Adicionar(carro);

        return CreatedAtAction(nameof(Listar), carro);
    }

    // Mostra o cálculo do valor da locação de um veículo.
    [HttpGet("{id}/valor")]
    public IActionResult ObterValor(int id)
    {
        var veiculo = _repository.ObterPorId(id);

        if (veiculo == null)
        {
            return NotFound();
        }

        DateTime hoje = DateTime.Today;

        int diasLocados = 0;
        decimal valorTotal = 0;

        if (veiculo.DataLocacao != null)
        {
            diasLocados = Math.Max(0, (hoje - veiculo.DataLocacao.Value).Days);
            valorTotal = veiculo.CalcularDiaria() * diasLocados;
        }

        DateTime? dataDevolucaoPrevista =
            veiculo.DataLocacao?.AddDays(Veiculo.PrazoLocacaoDias);

        return Ok(new
        {
            id = veiculo.Id,
            placa = veiculo.Placa,
            dataLocacao = veiculo.DataLocacao,
            dataDevolucaoPrevista = dataDevolucaoPrevista,
            diaria = veiculo.CalcularDiaria(),
            diasLocados = diasLocados,
            valorTotal = valorTotal
        });
    }
}
