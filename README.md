# MovieManagement

Aplicação de consola em C# para gestão de filmes, categorias e realizadores, com persistência simultânea em memória e SQLite.

---

## Estrutura do Projeto

```
MovieManagement/
├── MovieManagement.Domain        # Entidades e interfaces
├── MovieManagement.Business      # Lógica de negócio (Services)
├── MovieManagement.Data          # Repositórios (memória e SQLite)
└── MovieManagement.UI            # Interface de consola (Program + MenuUI)
```

### Camadas

| Camada | Responsabilidade |
|--------|-----------------|
| **Domain** | Define as entidades (`Movie`, `Category`, `Director`) e as interfaces dos repositórios |
| **Business** | Valida as regras de negócio antes de guardar ou alterar dados |
| **Data** | Implementa os repositórios, em memória (`List<T>`) e em SQLite |
| **UI** | Apresenta os menus e trata da interação com o utilizador |

---

## Funcionalidades

### Categorias
- Adicionar, listar e remover categorias

### Realizadores
- Adicionar, listar e remover realizadores

### Filmes
- Adicionar, listar, editar e remover filmes
- Listar com ordenação (título, ano ou classificação)
- Filtrar por categoria ou por realizador
- Pesquisar por título
- Relatório com estatísticas (classificação média, filme melhor avaliado, categoria mais popular, etc.)

---

## Regras de Negócio

- Não é possível adicionar um filme sem uma categoria e um realizador válidos
- O título de um filme não pode ser duplicado
- O ano tem de ser entre 1888 e o ano atual
- A classificação tem de ser entre 0 e 5
- Não é possível apagar uma categoria ou realizador que tenha filmes associados (garantido por foreign keys no SQLite)

---

## Persistência

O sistema persiste os dados simultaneamente em dois repositórios:

- **Memória** — os dados existem enquanto o programa está a correr
- **SQLite** — os dados ficam guardados no ficheiro `movies.db` e sobrevivem ao fecho do programa

A arquitetura permite trocar ou adicionar formas de persistência sem alterar a Business Layer ou a UI.

---

## Arquitetura

Cada camada comunica apenas com a camada abaixo através de interfaces:

```
UI → IMovieServices / ICategoryServices / IDirectorServices
Business → IMovieRepository / ICategoryRepository / IDirectorRepository
Data → implementa as interfaces acima
```

Isto garante que a UI e a Business Layer não dependem de implementações concretas, facilitando a manutenção e a extensão do projeto.

---

## Requisitos

- .NET 10
- Pacote `Microsoft.Data.Sqlite`

## Como correr

1. Abrir a solução no Visual Studio
2. Definir `MovieManagement.UI` como projeto de arranque
3. Correr com F5
