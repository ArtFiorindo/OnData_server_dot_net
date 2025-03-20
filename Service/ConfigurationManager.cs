using Microsoft.Extensions.Configuration;

namespace OnData.Services
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static readonly object _lock = new object();
        private readonly IConfiguration _configuration;

        private ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static ConfigurationManager Instance(IConfiguration configuration = null)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        if (configuration == null)
                        {
                            throw new ArgumentNullException(nameof(configuration), 
                                "IConfiguration must be provided when first initializing the ConfigurationManager");
                        }
                        _instance = new ConfigurationManager(configuration);
                    }
                    else if (configuration != null)
                    {
                        // Já existe uma instância, mas se um novo configuration foi fornecido, ignoramos
                        // Não alteramos a instância existente para manter o padrão Singleton
                    }
                }
            }
            return _instance;
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public T GetSection<T>(string sectionName) where T : class, new()
        {
            T section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }

        public string GetValue(string key)
        {
            return _configuration[key];
        }
    }
}