API de Gerenciamento de Vendas - Teste Técnico
Esta é a implementação de uma API RESTful para gerenciamento de vendas, desenvolvida como parte de um processo de avaliação técnica para a vaga de Engenheiro de Software Sênior.
O projeto foi desenvolvido com .NET 8.0 e segue os princípios de Clean Architecture, Domain-Driven Design (DDD) e CQRS.
Arquitetura
A solução está estruturada seguindo os princípios da Clean Architecture, com uma separação clara de responsabilidades entre as camadas:
Domain: Contém as entidades, regras de negócio e a lógica de domínio mais pura. É o coração da aplicação.
Application: Orquestra os casos de uso (features) utilizando o padrão CQRS com MediatR. Não contém lógica de negócio.
Infrastructure: Contém as implementações de interesses externos, como acesso a banco de dados (EF Core), logging, etc.
WebApi (Presentation): Expõe a funcionalidade da aplicação através de uma API RESTful.
Stack de Tecnologias
Backend: .NET 8.0, C#
Padrões: Clean Architecture, DDD, CQRS, SOLID
API: ASP.NET Core
Banco de Dados: PostgreSQL
ORM: Entity Framework Core 8
Bibliotecas Principais:
MediatR para implementação de CQRS.
FluentValidation para validação de dados.
AutoMapper para mapeamento de objetos.
Serilog para logging estruturado.


Testes:
xUnit como framework de teste.
NSubstitute para criação de mocks.
FluentAssertions para asserções legíveis.
Bogus para geração de dados de teste.


Como Configurar e Executar o Projeto
Pré-requisitos
.NET 8.0 SDK
Docker e Docker Compose
Passo 1: Configuração do Ambiente
O ambiente de desenvolvimento completo (API, banco de dados PostgreSQL) é orquestrado via Docker Compose.
Clone o repositório:
codeBash
git clone <url-do-repositorio>
cd <nome-da-pasta>


Configure as variáveis de ambiente (se necessário):
O arquivo docker-compose.yml está configurado com valores padrão para o banco de dados. Se precisar alterá-los, modifique as seções de ambiente. A connection string no appsettings.json está configurada para se conectar ao container Docker do PostgreSQL.
Passo 2: Executando com Docker Compose
A maneira mais simples de executar o projeto é usando o Docker Compose.
Inicie os containers:
Na raiz do projeto (onde o arquivo docker-compose.yml está localizado), execute o comando:
codeBash
docker-compose up --build
Este comando irá:
Construir a imagem Docker para a API.
Iniciar um container para a API.
Iniciar um container para o banco de dados PostgreSQL.


Acesse a API:
Após a inicialização, a API estará disponível em:
HTTP: http://localhost:8080
Documentação Swagger UI: http://localhost:8080/swagger


Passo 3: Aplicando as Migrações do Banco de Dados
Na primeira vez que executar o projeto, o banco de dados estará vazio. É necessário aplicar as migrações do Entity Framework Core para criar as tabelas.
Verifique se a API está em execução (via Docker).
Abra um novo terminal na raiz do projeto.
Execute o comando de atualização do banco de dados:
Certifique-se de ter as ferramentas do EF Core instaladas (dotnet tool install --global dotnet-ef).
codeBash
dotnet ef database update --project src/Ambev.DeveloperEvaluation.WebApi
Este comando irá ler as migrações e aplicar o schema no banco de dados que está rodando no container Docker.
Como Executar os Testes
Os testes unitários são fundamentais para garantir a qualidade do código.
Navegue até a pasta da solução:
Na raiz do projeto, onde o arquivo .sln está localizado.
Execute o comando de teste do .NET:
codeBash
dotnet test
Este comando irá descobrir e executar todos os testes nos projetos *.Unit.csproj, *.Integration.csproj, etc.
Estrutura dos Endpoints da API
A documentação completa dos endpoints, com exemplos de request e response, está disponível na interface do Swagger UI (http://localhost:8080/swagger).
POST /api/sales: Cria uma nova venda.
GET /api/sales/{id}: Busca uma venda pelo seu ID.
