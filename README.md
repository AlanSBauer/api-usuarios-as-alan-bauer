# API de Gerenciamento de Usuários

## 📘 Descrição

A API de Gerenciamento de Usuários foi desenvolvida como parte de uma avaliação acadêmica do curso de Análise e Desenvolvimento de Sistemas.  
Ela permite cadastrar, listar, atualizar e desativar usuários, aplicando boas práticas de arquitetura, validação e organização do código.  
O projeto utiliza .NET 8, Entity Framework Core com SQLite, FluentValidation e segue padrões como Repository, Services e DTOs.

Repositório: https://github.com/AlanSBauer/api-usuarios-as-alan-bauer

---

## 🛠 Tecnologias Utilizadas

- .NET 8.0
- ASP.NET Minimal API
- Entity Framework Core
- SQLite
- FluentValidation
- Swagger (OpenAPI)
- LINQ
- Dependency Injection

---

## 🧩 Padrões de Projeto Implementados

- Repository Pattern
- Service Pattern
- DTO Pattern
- Dependency Injection

---

## ▶ Como Executar o Projeto

## Pré-requisitos

- .NET SDK 8.0 instalado

## Passos para rodar

```bash
git clone https://github.com/AlanSBauer/api-usuarios-as-alan-bauer
cd api-usuarios-as-alan-bauer
dotnet run
```

---

## 📡 Exemplos de Requisições

## Criar Usuário (POST /usuarios)

{
"nome": "Alan Bauer",
"email": "alan@example.com",
"senha": "123456",
"dataNascimento": "2000-05-10",
"telefone": "11999999999"
}

---

## Atualizar Usuário (PUT /usuarios/{id})

{
"nome": "Alan Atualizado",
"email": "alan@example.com",
"dataNascimento": "2000-05-10",
"telefone": "11988887777",
"ativo": true
}

---

## 🗂 Estrutura do Projeto

/Application
/Dtos
/Interfaces
/Services
/Validators

/Domain
/Entities

/Infrastructure
/Persistence
/Repositories

Program.cs

---

## 👨‍💻 Autor

Alan Bauer

Curso: Análise e Desenvolvimento de Sistemas

## Link do Video:

url
