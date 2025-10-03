# Sales Management API - Technical Test

This is the implementation of a RESTful API for sales management, developed as part of a technical evaluation process for the **Senior Software Engineer** position.

The project was developed with **.NET 8.0** and strictly follows the principles of **Clean Architecture**, **Domain-Driven Design (DDD)**, and **CQRS**.

---

## Architecture

The solution is structured following the principles of **Clean Architecture**, with a clear separation of responsibilities between the layers:

-   🧅 **Domain**: Contains the entities, business rules, and the purest domain logic. It is the heart of the application.
-   🧅 **Application**: Orchestrates the use cases (features) using the CQRS pattern with `MediatR`. It does not contain business logic.
-   🧅 **Infrastructure**: Contains implementations of external concerns, such as database access (`EF Core`), logging, etc.
-   🧅 **WebApi (Presentation)**: Exposes the application's functionality through a RESTful API.

---

## Technology Stack

### Backend
-   **Framework**: .NET 8.0, C#
-   **Patterns**: Clean Architecture, DDD, CQRS, SOLID
-   **API**: ASP.NET Core
-   **Database**: PostgreSQL
-   **ORM**: Entity Framework Core 8

### Main Libraries
-   **`MediatR`**: For CQRS implementation.
-   **`FluentValidation`**: For data validation.
-   **`AutoMapper`**: For object mapping.
-   **`Serilog`**: For structured logging.

### Testing
-   **`xUnit`**: Testing framework.
-   **`NSubstitute`**: For creating mocks.
-   **`FluentAssertions`**: For readable assertions.
-   **`Bogus`**: For generating test data.

---

## How to Set Up and Run the Project

### Prerequisites
-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Docker](https://www.docker.com/products/docker-desktop) and Docker Compose

### Step 1: Environment Setup
The complete development environment (API and database) is orchestrated via Docker Compose.

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd <folder-name>
    ```

2.  **Environment Variables (Optional):**
    The `docker-compose.yml` and `appsettings.json` files are pre-configured to connect. If you need to change ports or credentials, adjust these files.

### Step 2: Running with Docker Compose

1.  **Start the containers:**
    In the project root, run the command:
    ```bash
    docker-compose up --build
    ```
    This command will build the images and start the API and PostgreSQL database containers.

2.  **Access the API:**
    After initialization, the API will be available at:
    -   **Base URL:** `http://localhost:8080`
    -   **Documentation (Swagger UI):** `http://localhost:8080/swagger`

### Step 3: Applying Database Migrations
On the first run, the database will be empty. You need to apply the migrations to create the tables.

1.  Make sure the Docker containers are running.
2.  Open a **new terminal** in the project root.
3.  Install the EF Core tool (if you haven't already):
    ```bash
    dotnet tool install --global dotnet-ef
    ```
4.  Run the command to update the database:
    ```bash
    dotnet ef database update --project src/Ambev.DeveloperEvaluation.WebApi
    ```
    This command will apply the schema to the database running in the container.

---

## How to Run the Tests

Unit tests are fundamental to ensuring code quality.

1.  **Navigate to the project root** (where the `.sln` file is located).
2.  **Run the .NET test command:**
    ```bash
    dotnet test
    ```
    This command will discover and run all tests in the solution.

---

## API Endpoint Structure

The complete documentation for the endpoints, with `request` and `response` examples, is available in the **Swagger UI** interface.

>  **`http://localhost:8080/swagger`**

### Main Sales Endpoints:
-   `POST /api/sales`: Creates a new sale.
-   `GET /api/sales/{id}`: Fetches a sale by its ID.