using GarageTagManagement.Repositories;
using GarageTagManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurando serviços do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrando o repositório como Singleton e o serviço como Scoped
builder.Services.AddSingleton<TagRepository>();  // Repositório em memória para manter estado entre requisições
builder.Services.AddScoped<TagService>();        // Serviço com ciclo de vida Scoped

// Adicionando suporte para controladores
builder.Services.AddControllers();

// Construindo a aplicação
var app = builder.Build();

// Middleware de logging para cada requisição
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started ");
    await next(context);
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Finished ");
});

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Desativando redirecionamento HTTPS (opcional)
app.UseHttpsRedirection();

// Mapeando as rotas da API
app.MapControllers();

// Executando a aplicação
app.Run();
