# On-Data Backend Server

API REST desenvolvida com C#/.NET usando Entity Framework para o aplicativo On-Data, uma solução de gerenciamento de sinistros odontológicos para a Odontoprev.

**Link GitHub**: [https://github.com/eduardofuncao/on-data-server-dotnet](https://github.com/eduardofuncao/on-data-server-dotnet)

---

## Equipe

> **Artur Lopes Fiorindo** » 53481  
> Responsável pela implementação do endpoint para **Ocorrências**
>
> **Eduardo Felipe Nunes Função** » 553362  
> Responsável pela implementação do endpoint para **Pacientes**
>
> **Jhoe Kochi Hashimoto** » 553831  
> Responsável pela ideação e definições de negócio do projeto

---

## Objetivo

Desenvolver uma API robusta e eficiente para gerenciar sinistros odontológicos, fornecendo endpoints CRUD para manipulação de entidades como Pacientes, Dentistas, Ocorrências e Doenças, integrada a um banco de dados Oracle e seguindo boas práticas de desenvolvimento em C#/.NET.

---

## Escopo

- **CRUD para Pacientes** (criação, leitura, atualização e exclusão)
- **CRUD para Ocorrências**
- Relacionamentos entre Pacientes, Dentistas, Ocorrências.

---

## Requisitos Funcionais

1. Operações de CRUD para entidades de Pacientes e Ocorrências.
2. Registro de ocorrências com detalhes como valor, data e status de aprovação.
3. API conectada a um banco de dados Oracle.
4. Suporte a operações de relacionamento entre entidades.

## Requisitos Não Funcionais

1. Desenvolvida em **C#/.NET Core**.
2. Conexão com banco de dados **Oracle**.
3. Aplicação modular e escalável, seguindo boas práticas.
4. Suporte a **Swagger** para documentação de endpoints.
5. Testes automatizados para funcionalidades essenciais.

---

## Arquitetura

O projeto utiliza uma arquitetura em camadas, seguindo o padrão **MVC (Model-View-Controller)** para a parte web e **Repository Pattern** para a camada de dados. A estrutura é dividida em:

### 1. **Camada de Apresentação (API)**
- **Controllers**: Expõem os endpoints REST e processam as solicitações HTTP.
- **Views**: Renderizam as páginas HTML para interação com o usuário.
- **Swagger**: Documentação automática da API.

### 2. **Camada de Negócio (Serviços)**
- **Services**: Implementam a lógica de negócios e regras de processamento.
- **ViewModels**: Classes que representam os dados exibidos nas views.

### 3. **Camada de Dados (Data Access Layer)**
- **Repositories**: Interagem com o banco de dados usando Entity Framework.
- **DbContext**: Configuração do contexto do banco de dados Oracle.

### Design Patterns Utilizados
- **Repository Pattern**: Para abstrair o acesso ao banco de dados e facilitar a manutenção.
- **Singleton**: Para gerenciar a configuração da aplicação.
- **Dependency Injection**: Para injeção de dependências nos serviços e controllers.

---

## Diagrama de Entidade-Relacionamento (ER)

![Diagrama ER](https://github.com/user-attachments/assets/a65fefc6-89d1-40ab-9486-03b65be135db)

---

## Configurações e Endpoints

### Configuração do Servidor

- **Arquivo Program.cs**: Define as configurações de inicialização, incluindo:
  - URLs (`http://localhost:5146` e `https://localhost:7114`)
  - Habilitação de **Swagger** para documentação
  - Configuração do banco de dados Oracle via `OnDataDbContext`
  - Mapeamento de rotas para controladores MVC e API

### Controllers

#### `CadastroController`

Gerencia o CRUD de Pacientes e Ocorrências com visualizações. Principais endpoints:

- **GET /Cadastro**: Exibe lista de pacientes e suas ocorrências.
- **GET /Cadastro/Create**: Formulário de criação de paciente e ocorrência.
- **POST /Cadastro/Create**: Processa a criação e salva no banco de dados.
- **GET /Cadastro/Details/{id}**: Exibe detalhes de um paciente e ocorrência.
- **GET /Cadastro/Edit/{id}** e **POST /Cadastro/Edit/{id}**: Edição de dados.
- **GET /Cadastro/Delete/{id}** e **POST /Cadastro/Delete/{id}**: Confirma e processa exclusão.

#### `OcorrenciaController`

Responsável pelas operações de CRUD para ocorrências via API.

- **GET /api/Ocorrencias**: Retorna lista de ocorrências.
- **GET /api/Ocorrencias/{id}**: Retorna uma ocorrência específica.
- **PUT /api/Ocorrencias/{id}**: Atualiza dados de uma ocorrência.
- **POST /api/Ocorrencias**: Cria uma nova ocorrência.
- **DELETE /api/Ocorrencias/{id}**: Exclui uma ocorrência.

#### `PacienteController`

Gerencia CRUD para pacientes via API.

- **GET /api/Pacientes**: Retorna lista de pacientes.
- **GET /api/Pacientes/{id}**: Retorna um paciente específico.
- **PUT /api/Pacientes/{id}**: Atualiza dados de um paciente.
- **POST /api/Pacientes**: Cria um novo paciente.
- **DELETE /api/Pacientes/{id}**: Exclui um paciente.

#### `HomeController`

Renderiza a página inicial da aplicação.

- **GET /Home/Index**: Exibe a página inicial com links para criar e listar pacientes e ocorrências.

---

## Como Executar o Projeto

### Pré-requisitos
- **.NET SDK 8.0** instalado.
- **Oracle Database** configurado e acessível.
- **Git** para clonar o repositório.

### Passos para Execução

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/eduardofuncao/on-data-server-dotnet.git
   cd on-data-server-dotnet
   ```

2. **Configure o banco de dados**:
  - Atualize a string de conexão no arquivo `appsettings.json` com as credenciais do seu banco de dados Oracle.

3. **Execute as migrations**:
   ```bash
   dotnet-ef database update
   ```

4. **Inicie o servidor**:
   ```bash
   dotnet run
   ```

5. **Acesse a API**:
  - Acesse `http://localhost:5146` para a interface web.
  - Acesse `http://localhost:5146/swagger` para a documentação da API.

---

## Testes

### Testes Automatizados
Foram implementados testes unitários para os serviços e controllers usando **xUnit**. Para executar os testes, use o comando:

```bash
dotnet test
```

### Exemplos de Testes

#### Teste de Criação de Paciente
```csharp
[Fact]
public async Task CreatePaciente_ShouldAddPacienteToDatabase()
{
    var options = new DbContextOptionsBuilder<OnDataDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

    using (var context = new OnDataDbContext(options))
    {
        var service = new PacienteService(context);
        var paciente = new Paciente { Nome = "Teste", Cpf = "123.456.789-00" };

        await service.CreatePacienteAsync(paciente);

        var result = await context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == "123.456.789-00");
        Assert.NotNull(result);
        Assert.Equal("Teste", result.Nome);
    }
}
```

#### Teste de Listagem de Ocorrências
```csharp
[Fact]
public async Task GetOcorrencias_ShouldReturnListOfOcorrencias()
{
    var options = new DbContextOptionsBuilder<OnDataDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

    using (var context = new OnDataDbContext(options))
    {
        context.Ocorrencias.Add(new Ocorrencia { NomeOcorrencia = "Teste", Detalhes = "Detalhes" });
        await context.SaveChangesAsync();

        var service = new OcorrenciaService(context);
        var result = await service.GetOcorrenciasAsync();

        Assert.Single(result);
        Assert.Equal("Teste", result[0].NomeOcorrencia);
    }
}
```

---

## Conclusão

Este projeto demonstra a aplicação de boas práticas de desenvolvimento em C#/.NET, incluindo arquitetura em camadas, design patterns e testes automatizados. A API está pronta para ser integrada ao frontend e utilizada em produção.

---