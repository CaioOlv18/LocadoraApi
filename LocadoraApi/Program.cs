using LocadoraApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura os controllers da API.
builder.Services.AddControllers();

// Configura o Swagger para testar a API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra o Repository para ser usado pelos controllers.
builder.Services.AddSingleton<IVeiculoRepository, VeiculoRepository>();

// Permite qualquer origem, método e cabeçalho.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// O Swagger fica disponível no ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// O CORS precisa vir antes do mapeamento dos controllers.
app.UseCors("PermitirTudo");

app.MapControllers();

app.Run();
