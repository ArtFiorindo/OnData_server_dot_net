using Microsoft.EntityFrameworkCore;
using OnData.Data;
using OnData.Services;
using ConfigurationManager = OnData.Services.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

// Inicializar o ConfigurationManager como Singleton
var configManager = ConfigurationManager.Instance(builder.Configuration);

// Configuração das URLs, incluindo as portas HTTP e HTTPS
builder.WebHost.UseUrls("http://localhost:5146", "https://localhost:7114");

// Configuração dos serviços, agora com suporte a Views
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnDataDbContext>(options =>
    options.UseOracle(configManager.GetConnectionString("OracleDbConnection")));

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita o uso de arquivos estáticos na pasta wwwroot
app.UseStaticFiles();

// Habilitar o redirecionamento para HTTPS - Comentado temporariamente para diagnóstico
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();