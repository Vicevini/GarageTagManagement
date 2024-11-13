using GarageTagManagement.Repositories;
using GarageTagManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurando serviços do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrando o serviço e o repositório para o gerenciamento de tags
builder.Services.AddScoped<TagService, TagService>();
builder.Services.AddScoped<TagRepository, TagRepository>();

// Adicionando suporte para controladores
builder.Services.AddControllers();

// Construindo a aplicação
var app = builder.Build();

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

// Desativando redirecionamento HTTPS para evitar erro de porta (opcional)
app.UseHttpsRedirection();

// Mapeando as rotas da API
app.MapControllers();

app.Run();
