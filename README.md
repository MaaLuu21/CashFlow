<h1 align="center">CashFlow</h1>

### Sobre o projeto

Esta API, desenvolvida utilizando **.NET 8**, adota os princípios do **Domain-Driven Design (DDD)** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalhando informações como título, data e hora, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em um banco de dados **MySQL**.

A arquitetura da **API baseia-se em REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas **SQL**.

<p align="center">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white"/>
  <img src="https://img.shields.io/badge/.NET-8%2B-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/Versão-1.0-blueviolet?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/Contribuições-Bem%20vindas-brightgreen?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/Status-%20desenvolvimento-red?style=for-the-badge"/>
</p>

### Funcionalidades

- **Domain-Driven Design (DDD)**: Organização modular que torna mais simples compreender e manter o domínio da aplicação.
- **Testes de Unidade**: Cobertura de testes com FluentAssertions para assegurar a correta funcionalidade e a qualidade do código.
- **Geração de Relatórios**: Possibilidade de gerar relatórios completos em PDF e Excel, proporcionando uma visualização clara e eficiente das despesas.
- **RESTful API com Documentação Swagger**: Documentação interativa que facilita a integração e o uso da API por outros desenvolvedores.

### ⚙️ Como Executar o Projeto
#### Requisitos

- [.NET SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) (versão 8.0 ou superior). 
- Um editor de código como Visual Studio Code ou Visual Studio 2022.
- MySql Server

1. 💻 **Clonando o repositório**
   ```bash
   git clone https://github.com/MaaLuu21/CashFlow
   ```

2. **Preencha o `appsettings.Development.json` ex:**
    ```json
    {
      "ConnectionStrings": {
        "connection": "Server=localhost;Database=cashflowdb;Uid=root;Pwd=yourpwd;"
      }
    }
    ```

3. 📂 **Acesse o diretório do projeto**
    ```bash
    cd C:\CashFlow
    ```
4. 🧰 **Restaure as dependências e compile**
    ```bash
    dotnet restore
    dotnet build
    ```
5. ▶️ **Executando**
    ```bash
    dotnet run
    ```
