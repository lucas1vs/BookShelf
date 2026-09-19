📚 BookShelf

API REST para gerenciamento de biblioteca, desenvolvida em C# / .NET com Entity Framework Core e SQL Server.

O sistema permite o cadastro e controle de livros, autores, exemplares físicos, clientes e empréstimos, aplicando modelagem de banco de dados relacional e boas práticas de arquitetura em camadas.

🚀 Tecnologias
C# / .NET
Entity Framework Core
SQL Server
ASP.NET Core Web API
LINQ
Swagger / OpenAPI
🗂️ Estrutura do domínio

O projeto é organizado em camadas (Domain, EFCore, API), com as seguintes entidades principais:

Author — autores dos livros
Book — livros cadastrados, vinculados a um autor
BookCopy — exemplares físicos de um livro (controle de disponibilidade e condição)
Client — clientes cadastrados na biblioteca
Address — endereços, que podem ser compartilhados entre clientes
Loan — empréstimos, relacionando um cliente a um exemplar
🔗 Endpoints disponíveis
Método	Rota	Descrição
GET	/api/books	Lista todos os livros
GET	/api/books/{id}	Busca um livro pelo Id (404 se não existir)

Mais endpoints em desenvolvimento (Authors, Clients, Loans).

⚙️ Como rodar o projeto
Clone o repositório
Configure a connection string do SQL Server em AppConfig
Rode as migrations do Entity Framework Core:
bash
   dotnet ef database update
Execute o projeto (F5 no Visual Studio ou dotnet run)
Acesse a documentação interativa via Swagger (/swagger)
🌱 Seed de dados

O projeto conta com um DataSeeder, responsável por popular o banco com dados de exemplo (autores, livros, endereços, clientes e exemplares) na primeira execução, evitando duplicidade em execuções seguintes.

👤 Autor

Desenvolvido por Lucas Domingos — Desenvolvedor Backend focado em C#, .NET e SQL Server.
