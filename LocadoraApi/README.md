# Sistema de Locadora de Veículos

Projeto desenvolvido para a atividade prática da disciplina de **Programação Orientada a Objetos**, do curso de Engenharia de Software.

A aplicação é uma Web API simples para cadastrar e consultar veículos de uma locadora. O sistema trabalha com dois tipos de veículos: carros e motos. Cada tipo possui suas próprias características e valor de diária.

## Objetivo do projeto

O objetivo é praticar alguns conceitos de Programação Orientada a Objetos usando C#:

- **Herança:** `Carro` e `Moto` herdam as características da classe `Veiculo`.
- **Abstração:** `Veiculo` é uma classe abstrata e define o método `CalcularDiaria()`.
- **Polimorfismo:** cada tipo de veículo implementa sua própria versão de `CalcularDiaria()` e `Descricao()`.
- **Interface:** `Carro` implementa `IRastreavel`, enquanto `Moto` não possui rastreamento.
- **Repository:** a interface `IVeiculoRepository` e a classe `VeiculoRepository` cuidam do armazenamento e da consulta dos veículos.

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core Web API
- Swagger / Swashbuckle
- Visual Studio Code

## Organização das pastas

```text
LocadoraApi/
├── Controllers/
│   └── VeiculosController.cs
├── Models/
│   ├── IRastreavel.cs
│   ├── Veiculo.cs
│   ├── Carro.cs
│   └── Moto.cs
├── Repositories/
│   ├── IVeiculoRepository.cs
│   └── VeiculoRepository.cs
├── Properties/
│   └── launchSettings.json
├── LocadoraApi.csproj
├── Program.cs
└── README.md
```

## Veículos que já vêm cadastrados

O Repository começa com dois veículos cadastrados:

| ID | Tipo | Placa | Modelo | Ano | Informação específica |
|---|---|---|---|---:|---|
| 1 | Carro | ABC1D23 | Gol | 2020 | 4 portas |
| 2 | Moto | XYZ9K88 | CG 160 | 2022 | 160 cilindradas |

O Gol está com uma locação iniciada em **16/11/2026**. A moto está sem locação.

## Valores das diárias

| Tipo | Valor por dia |
|---|---:|
| Carro | R$ 150,00 |
| Moto | R$ 80,00 |

O prazo de locação definido no projeto é de 3 dias. O valor total é calculado de acordo com a quantidade de dias desde a data da locação até o dia da consulta.

## Como executar o projeto

### Pré-requisito

É necessário ter o SDK do **.NET 10** instalado.

### Passos

1. Baixe ou clone este repositório.
2. Abra a pasta `LocadoraApi` no Visual Studio Code.
3. Abra o terminal dentro da pasta do projeto.
4. Restaure os pacotes com o comando:

```bash
dotnet restore
```

5. Inicie a API:

```bash
dotnet run
```

6. No terminal, confira o endereço local informado pela aplicação e abra esse endereço no navegador, acrescentando `/swagger`.

Exemplo: `http://localhost:5000/swagger`.

## Endpoints disponíveis

| Método | Rota | Descrição | Respostas esperadas |
|---|---|---|---|
| GET | `/api/Veiculos` | Lista todos os veículos cadastrados. | 200 OK |
| POST | `/api/Veiculos/carro` | Cadastra um novo carro. | 201 Created ou 400 Bad Request |
| GET | `/api/Veiculos/{id}/valor` | Consulta os dados e o valor da locação de um veículo. | 200 OK ou 404 Not Found |

## Exemplo de cadastro de carro

No Swagger, abra o endpoint `POST /api/Veiculos/carro` e use este corpo JSON:

```json
{
  "placa": "QWE4R56",
  "modelo": "Argo",
  "anoFabricacao": 2024,
  "numeroPortas": 4
}
```

O ID não precisa ser enviado. O Repository gera o próximo número automaticamente.

## Regras de validação

No cadastro de um carro, a API verifica:

1. A placa não pode ficar vazia ou conter somente espaços.
2. O modelo não pode ficar vazio ou conter somente espaços.
3. O ano de fabricação deve estar entre 1900 e o ano atual mais 1.
4. O número de portas deve estar entre 2 e 5.

Quando algum dado não atende às regras, a API retorna `400 Bad Request` com uma mensagem explicando o problema.

## Testes pelo Swagger

Os testes previstos na atividade são:

1. Listar os veículos e conferir o Gol e a CG 160.
2. Cadastrar um carro usando o exemplo de JSON.
3. Listar novamente e conferir se o novo carro recebeu o ID 3.
4. Tentar cadastrar um carro com dados inválidos e conferir o erro 400.
5. Consultar `/api/Veiculos/1/valor` e conferir o cálculo da locação do Gol.
6. Consultar `/api/Veiculos/2/valor` e conferir que a moto sem locação apresenta zero dias e valor total zero.
7. Consultar `/api/Veiculos/99/valor` e conferir a resposta 404.

## Observação

Os dados ficam armazenados em memória usando um `Dictionary<int, Veiculo>`. Por isso, os veículos cadastrados durante a execução não são mantidos quando a aplicação é encerrada.

---

**Projeto acadêmico — Sistema de Locadora de Veículos.**
