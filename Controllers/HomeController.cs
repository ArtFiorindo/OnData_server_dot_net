using Microsoft.AspNetCore.Mvc;
using OnData.Services;
using ConfigurationManager = OnData.Services.ConfigurationManager;

namespace OnData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConfigurationManager _configManager;

        public HomeController(IConfiguration configuration)
        {
            // Obter a instância do ConfigurationManager
            _configManager = ConfigurationManager.Instance(configuration);
        }

        public IActionResult Index()
        {
            // Exemplo de uso do ConfigurationManager
            ViewBag.ConnectionString = _configManager.GetConnectionString("OracleDbConnection");
            return View();
        }
    }
}