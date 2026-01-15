## 📚 Sobre o Projeto FiapCloudGames

**FIAP Cloud Games** é uma plataforma de jogos desenvolvida como projeto avaliativo para 1 etapa da pós graduação da instituição **FIAP**. .

O projeto tem como objetivo proporcionar uma experiência prática no desenvolvimento de **APIs modernas**, aplicando conceitos como:

- Arquitetura em camadas (Clean Architecture)
- Autenticação e autorização com JWT
- Persistência de dados com Entity Framework Core
- Testes escritos
- Logs estruturados
- Exceções customizadas
A aplicação simula um ambiente de gerenciamento de jogos e usuários, com controle de acesso baseado em **roles** (Admin e User).

---

## 🏗️ Arquitetura da Solução

A solução está organizada nas seguintes camadas:

- **Api**  
  Camada de entrada da aplicação. Contém Controllers, Middlewares, configurações, Swagger e o entry point.

- **Application**  
  Contém Services, DTOs e regras de aplicação.

- **Domain**  
  Contém Entidades, Value Objects, Interfaces e Exceções de domínio.

- **Infrastructure**  
  Responsável pelo acesso a dados, repositórios, DbContext, Entity Framework Core e ASP.NET Identity.

- **Tests**  
  Testes unitários e de infraestrutura utilizando **xUnit**.

---

## ⚙️ Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [ASP.NET Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity)
- [JWT (JSON Web Token)](https://jwt.io/)
- [SQL Server 2019 / Express](https://www.microsoft.com/sql-server)
- [Swagger / OpenAPI](https://swagger.io/)
- [xUnit](https://xunit.net/)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/pt-br/)

---

## 🔐 Autenticação e Autorização

- Autenticação baseada em **JWT**
- Controle de acesso por **Roles**
  - **Admin** → gerenciamento completo (criação, atualização e exclusão)
  - **User** → acesso a recursos públicos
- Proteção de endpoints com:
  - `[Authorize]`
  - `[Authorize(Roles = "Admin")]`

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos

Certifique-se de ter instalado:

- **.NET SDK 9**
- **SQL Server 2019 ou SQL Server Express**
- **Visual Studio 2022 ou superior**

---

### Passo a Passo

1. Clone o repositório:
```bash
2. git clone <url-do-repositorio>
3. Abra a solução no Visual Studio.
4. Abra o Console do Gerenciador de Pacotes.
5. Altere o Projeto Padrão para Infrastructure.
6. Crie a migração inicial do banco: Add-Migration InitialIdentity
7. Aplique a migração no banco de dados: Update-Database
8. Defina o projeto Api como projeto de inicialização.
9. Execute a aplicação utilizando IIS Express ou Kestrel.
10. O Swagger será aberto automaticamente no navegador.
```

---

## 🧪 Executando os Testes

Os testes do projeto foram desenvolvidos utilizando **xUnit**, garantindo a validação das regras de negócio e da camada de infraestrutura.

### Tipos de Testes

- **Testes de Repositório**
  - Validação de operações de criação, atualização e exclusão
  - Utilização de banco em memória (`InMemoryDatabase`) para isolamento dos testes

- **Testes de Serviços**
  - Validação das regras de negócio

---

### Executando os Testes no Visual Studio

1. Abra a solução no **Visual Studio**
2. Acesse o menu **Test > Test Explorer**
3. Clique em **Run All Tests**

---

📄 Documentação da API
A documentação da API é gerada automaticamente via Swagger e pode ser acessada em: **https://localhost:{porta}/swagger**

📌 Observações
Este projeto foi desenvolvido com fins educacionais, focando na aplicação de boas práticas de backend, organização de código, autenticação segura e testes automatizados.
