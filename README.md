

# 🛍️ API de Gerenciamento de Vendas - Teste Técnico

Esta é a implementação de uma API RESTful para gerenciamento de vendas, desenvolvida como parte de um processo de avaliação técnica para a vaga de **Engenheiro de Software Sênior**.

O projeto foi desenvolvido com **.NET 8.0** e segue rigorosamente os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **CQRS**.

---

## 🏛️ Arquitetura

A solução está estruturada seguindo os princípios da **Clean Architecture**, com uma separação clara de responsabilidades entre as camadas:

-   🧅 **Domain**: Contém as entidades, regras de negócio e a lógica de domínio mais pura. É o coração da aplicação.
-   🧅 **Application**: Orquestra os casos de uso (features) utilizando o padrão CQRS com `MediatR`. Não contém lógica de negócio.
-   🧅 **Infrastructure**: Contém as implementações de interesses externos, como acesso a banco de dados (`EF Core`), logging, etc.
-   🧅 **WebApi (Presentation)**: Expõe a funcionalidade da aplicação através de uma API RESTful.

---

## 🛠️ Stack de Tecnologias

### Backend
-   **Framework**: .NET 8.0, C#
-   **Padrões**: Clean Architecture, DDD, CQRS, SOLID
-   **API**: ASP.NET Core
-   **Banco de Dados**: PostgreSQL
-   **ORM**: Entity Framework Core 8

### Bibliotecas Principais
-   **`MediatR`**: Para implementação de CQRS.
-   **`FluentValidation`**: Para validação de dados.
-   **`AutoMapper`**: Para mapeamento de objetos.
-   **`Serilog`**: Para logging estruturado.

### Testes
-   **`xUnit`**: Framework de teste.
-   **`NSubstitute`**: Para criação de mocks.
-   **`FluentAssertions`**: Para asserções legíveis.
-   **`Bogus`**: Para geração de dados de teste.

---

## 🚀 Como Configurar e Executar o Projeto

### Pré-requisitos
-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Docker](https://www.docker.com/products/docker-desktop) e Docker Compose

### Passo 1: Configuração do Ambiente
O ambiente de desenvolvimento completo (API e banco de dados) é orquestrado via Docker Compose.

1.  **Clone o repositório:**
    ```bash
    git clone <url-do-repositorio>
    cd <nome-da-pasta>
    ```

2.  **Variáveis de Ambiente (Opcional):**
    O arquivo `docker-compose.yml` e o `appsettings.json` estão pré-configurados para se conectarem. Se precisar alterar portas ou credenciais, ajuste esses arquivos.

### Passo 2: Executando com Docker Compose
Esta é a maneira mais simples e recomendada de executar o projeto.

1.  **Inicie os containers:**
    Na raiz do projeto, execute o comando:
    ```bash
    docker-compose up --build
    ```
    Este comando irá construir as imagens e iniciar os containers da API e do banco de dados PostgreSQL.

2.  **Acesse a API:**
    Após a inicialização, a API estará disponível em:
    -   **URL Base:** `http://localhost:8080`
    -   **Documentação (Swagger UI):** `http://localhost:8080/swagger`

### Passo 3: Aplicando as Migrações do Banco de Dados
Na primeira execução, o banco de dados estará vazio. É necessário aplicar as migrações para criar as tabelas.

1.  Certifique-se de que os containers do Docker estão em execução.
2.  Abra um **novo terminal** na raiz do projeto.
3.  Instale a ferramenta do EF Core (se ainda não tiver):
    ```bash
    dotnet tool install --global dotnet-ef
    ```
4.  Execute o comando para atualizar o banco de dados:
    ```bash
    dotnet ef database update --project src/Ambev.DeveloperEvaluation.WebApi
    ```
    Este comando aplicará o schema no banco de dados que está rodando no container.

---

## ✅ Como Executar os Testes

Os testes unitários são fundamentais para garantir a qualidade do código.

1.  **Navegue até a raiz do projeto** (onde o arquivo `.sln` está localizado).
2.  **Execute o comando de teste do .NET:**
    ```bash
    dotnet test
    ```
    Este comando irá descobrir e executar todos os testes da solução.

---

## 🗺️ Estrutura dos Endpoints da API

A documentação completa dos endpoints, com exemplos de `request` e `response`, está disponível na interface do **Swagger UI**.

> 👉 **`http://localhost:8080/swagger`**

### Principais Endpoints de Vendas:
-   `POST /api/sales`: Cria uma nova venda.
-   `GET /api/sales/{id}`: Busca uma venda pelo seu ID.
