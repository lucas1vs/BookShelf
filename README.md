# BookShelf

Aplicação Console em .NET para gerenciamento de acervo e empréstimos de uma biblioteca pessoal.

## Arquitetura

- `BookShelf.Domain`: entidades e enums do domínio.
- `BookShelf.EFCore`: `DbContext` e mapeamentos do EF Core.
- `BookShelf`: aplicação Console.

## Modelo atual

O domínio possui as entidades `Author`, `Book`, `BookCopy`, `Client`, `Address` e `Loan`.

Relações planejadas:

```text
Author  1 ─── N Book 1 ─── N BookCopy 1 ─── N Loan N ─── 1 Client
Address 1 ─── N Client
```

## Diário de desenvolvimento

### 14/09/2026

- Estruturadas as entidades e enums do domínio.
- Adicionadas `BookCopy` e `Loan` para representar exemplares físicos e histórico de empréstimos.
- Registrados os `DbSet`s no `AppDbContext`.
- Iniciadas as configurações de propriedades no `OnModelCreating`.
- Criado o commit `171a12e` (`feat: add library domain model`).

## Próxima sessão

1. Aprender e aplicar `HasCheckConstraint` para garantir `Book.Pages > 0`.
2. Concluir as configurações de propriedades: conversão dos enums para texto, limites e obrigatoriedade.
3. Mapear os relacionamentos com Fluent API e definir os comportamentos de exclusão.
4. Corrigir os avisos de nulabilidade restantes em `Address`.
