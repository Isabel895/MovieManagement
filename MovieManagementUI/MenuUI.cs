using MovieManagement.Business.Services;
using MovieManagement.Domain.Entities;

namespace MovieManagement.UI
{
    internal class MenuUI
    {
        private readonly ICategoryServices _categoryService;
        private readonly ICategoryServices _categorySQLService;
        private readonly IDirectorServices _directorService;
        private readonly IDirectorServices _directorSQLService;
        private readonly IMovieServices _movieService;
        private readonly IMovieServices _movieSQLService;

        public MenuUI(
            ICategoryServices categoryService,
            ICategoryServices categorySQLService,
            IDirectorServices directorService,
            IDirectorServices directorSQLService,
            IMovieServices movieService,
            IMovieServices movieSQLService)
        {
            _categoryService = categoryService;
            _categorySQLService = categorySQLService;
            _directorService = directorService;
            _directorSQLService = directorSQLService;
            _movieService = movieService;
            _movieSQLService = movieSQLService;
        }

        public void Executar()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=== Movie Management ===");
                Console.WriteLine("1. Gerir Categorias");
                Console.WriteLine("2. Gerir Realizadores");
                Console.WriteLine("3. Gerir Filmes");
                Console.WriteLine("0. Sair");
                Console.Write("\nOpcao: ");
                string op = Console.ReadLine() ?? "";

                switch (op)
                {
                    case "1": MenuCategorias(); break;
                    case "2": MenuRealizadores(); break;
                    case "3": MenuFilmes(); break;
                    case "0": sair = true; break;
                    default: Console.WriteLine("Opcao invalida."); Pausa(); break;
                }
            }
        }

        //  CATEGORIAS
        private void MenuCategorias()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Categorias ===");
                Console.WriteLine("1. Adicionar categoria");
                Console.WriteLine("2. Listar categorias");
                Console.WriteLine("3. Remover categoria");
                Console.WriteLine("0. Voltar");
                Console.Write("\nOpcao: ");
                string op = Console.ReadLine() ?? "";

                switch (op)
                {
                    case "1": AdicionarCategoria(); break;
                    case "2": ListarCategorias(); break;
                    case "3": RemoverCategoria(); break;
                    case "0": voltar = true; break;
                }
            }
        }

        private void AdicionarCategoria()
        {
            Console.Write("Nome da categoria: ");
            string nome = Console.ReadLine() ?? "";
            try
            {
                _categoryService.Adicionar(nome);
                _categorySQLService.Adicionar(nome);
                Console.WriteLine("Categoria adicionada com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
            Pausa();
        }

        private void ListarCategorias()
        {
            Console.WriteLine("\n--- Categorias (Memoria) ---");
            var cats = _categoryService.Listar();
            if (cats.Count == 0) Console.WriteLine("Sem categorias.");
            foreach (var c in cats)
                Console.WriteLine($"  [{c.ID}] {c.Nome}");

            Console.WriteLine("\n--- Categorias (SQLite) ---");
            var catsSql = _categorySQLService.Listar();
            if (catsSql.Count == 0) Console.WriteLine("Sem categorias.");
            foreach (var c in catsSql)
                Console.WriteLine($"  [{c.ID}] {c.Nome}");

            Pausa();
        }

        private void RemoverCategoria()
        {
            int id = LerInteiro("ID da categoria a remover: ");
            try { _categoryService.Remover(id); Console.WriteLine("Removido da memoria."); }
            catch (Exception ex) { Console.WriteLine($"Memoria: {ex.Message}"); }

            try { _categorySQLService.Remover(id); Console.WriteLine("Removido do SQLite."); }
            catch (Exception ex) { Console.WriteLine($"SQLite: {ex.Message}"); }
            Pausa();
        }

        //  REALIZADORES
        private void MenuRealizadores()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Realizadores ===");
                Console.WriteLine("1. Adicionar realizador");
                Console.WriteLine("2. Listar realizadores");
                Console.WriteLine("3. Remover realizador");
                Console.WriteLine("0. Voltar");
                Console.Write("\nOpcao: ");
                string op = Console.ReadLine() ?? "";

                switch (op)
                {
                    case "1": AdicionarRealizador(); break;
                    case "2": ListarRealizadores(); break;
                    case "3": RemoverRealizador(); break;
                    case "0": voltar = true; break;
                }
            }
        }

        private void AdicionarRealizador()
        {
            Console.Write("Nome do realizador: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Pais: ");
            string pais = Console.ReadLine() ?? "";
            try
            {
                _directorService.Adicionar(nome, pais);
                _directorSQLService.Adicionar(nome, pais);
                Console.WriteLine("Realizador adicionado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
            Pausa();
        }

        private void ListarRealizadores()
        {
            Console.WriteLine("\n--- Realizadores (Memoria) ---");
            var dirs = _directorService.Listar();
            if (dirs.Count == 0) Console.WriteLine("Sem realizadores.");
            foreach (var d in dirs)
                Console.WriteLine($"  [{d.ID}] {d.Nome} ({d.Pais})");

            Console.WriteLine("\n--- Realizadores (SQLite) ---");
            var dirsSql = _directorSQLService.Listar();
            if (dirsSql.Count == 0) Console.WriteLine("Sem realizadores.");
            foreach (var d in dirsSql)
                Console.WriteLine($"  [{d.ID}] {d.Nome} ({d.Pais})");

            Pausa();
        }

        private void RemoverRealizador()
        {
            int id = LerInteiro("ID do realizador a remover: ");
            try { _directorService.Remover(id); Console.WriteLine("Removido da memoria."); }
            catch (Exception ex) { Console.WriteLine($"Memoria: {ex.Message}"); }

            try { _directorSQLService.Remover(id); Console.WriteLine("Removido do SQLite."); }
            catch (Exception ex) { Console.WriteLine($"SQLite: {ex.Message}"); }
            Pausa();
        }

        //  FILMES
        private void MenuFilmes()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Filmes ===");
                Console.WriteLine("1. Adicionar filme");
                Console.WriteLine("2. Listar filmes");
                Console.WriteLine("3. Editar filme");
                Console.WriteLine("4. Remover filme");
                Console.WriteLine("5. Pesquisar por titulo");
                Console.WriteLine("0. Voltar");
                Console.Write("\nOpcao: ");
                string op = Console.ReadLine() ?? "";

                switch (op)
                {
                    case "1": AdicionarFilme(); break;
                    case "2": ListarFilmes(); break;
                    case "3": EditarFilme(); break;
                    case "4": RemoverFilme(); break;
                    case "5": PesquisarFilme(); break;
                    case "0": voltar = true; break;
                }
            }
        }

        private void AdicionarFilme()
        {
            var categorias = _categoryService.Listar();
            var realizadores = _directorService.Listar();

            if (categorias.Count == 0)
            {
                Console.WriteLine("Nao existem categorias. Adiciona uma categoria primeiro.");
                Pausa(); return;
            }
            if (realizadores.Count == 0)
            {
                Console.WriteLine("Nao existem realizadores. Adiciona um realizador primeiro.");
                Pausa(); return;
            }

            Console.Write("Titulo: ");
            string titulo = Console.ReadLine() ?? "";
            int ano = LerInteiroEntre("Ano: ", 1888, DateTime.Now.Year);
            Console.Write("Lingua: ");
            string lingua = Console.ReadLine() ?? "";
            int classificacao = LerInteiroEntre("Classificacao (0-5): ", 0, 5);

            Console.WriteLine("\nCategorias disponiveis:");
            foreach (var c in categorias) Console.WriteLine($"  [{c.ID}] {c.Nome}");
            int categoriaId = LerInteiro("ID da Categoria: ");

            Console.WriteLine("\nRealizadores disponiveis:");
            foreach (var d in realizadores) Console.WriteLine($"  [{d.ID}] {d.Nome}");
            int realizadorId = LerInteiro("ID do Realizador: ");

            try
            {
                _movieService.Adicionar(titulo, ano, lingua, classificacao, categoriaId, realizadorId);
                _movieSQLService.Adicionar(titulo, ano, lingua, classificacao, categoriaId, realizadorId);
                Console.WriteLine("Filme adicionado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
            Pausa();
        }

        private void EditarFilme()
        {
            ListarFilmes(pausar: false);

            int id = LerInteiro("ID do filme a editar: ");

            var categorias = _categoryService.Listar();
            var realizadores = _directorService.Listar();

            Console.Write("Novo titulo: ");
            string titulo = Console.ReadLine() ?? "";
            int ano = LerInteiroEntre("Novo ano: ", 1888, DateTime.Now.Year);
            Console.Write("Nova lingua: ");
            string lingua = Console.ReadLine() ?? "";
            int classificacao = LerInteiroEntre("Nova classificacao (0-5): ", 0, 5);

            Console.WriteLine("\nCategorias disponiveis:");
            foreach (var c in categorias) Console.WriteLine($"  [{c.ID}] {c.Nome}");
            int categoriaId = LerInteiro("ID da Categoria: ");

            Console.WriteLine("\nRealizadores disponiveis:");
            foreach (var d in realizadores) Console.WriteLine($"  [{d.ID}] {d.Nome}");
            int realizadorId = LerInteiro("ID do Realizador: ");

            try
            {
                _movieService.Editar(id, titulo, ano, lingua, classificacao, categoriaId, realizadorId);
                _movieSQLService.Editar(id, titulo, ano, lingua, classificacao, categoriaId, realizadorId);
                Console.WriteLine("Filme editado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
            Pausa();
        }

        private void ListarFilmes(bool pausar = true)
        {
            // Usar SQLite como fonte de nomes (persiste entre sessoes)
            var categorias = _categorySQLService.Listar();
            var realizadores = _directorSQLService.Listar();

            string NomeCategoria(int id) => categorias.Find(c => c.ID == id)?.Nome ?? $"[ID {id}]";
            string NomeRealizador(int id) => realizadores.Find(d => d.ID == id)?.Nome ?? $"[ID {id}]";

            Console.WriteLine("\n--- Filmes (Memoria) ---");
            var filmes = _movieService.Listar();
            if (filmes.Count == 0) Console.WriteLine("Sem filmes.");
            foreach (var m in filmes)
                Console.WriteLine($"  [{m.ID}] {m.Titulo} ({m.Ano}) | {m.Lingua} | {m.Classificacao}/5 | {NomeCategoria(m.CategoriaId)} | {NomeRealizador(m.RealizadorId)}");

            Console.WriteLine("\n--- Filmes (SQLite) ---");
            var filmesSql = _movieSQLService.Listar();
            if (filmesSql.Count == 0) Console.WriteLine("Sem filmes.");
            foreach (var m in filmesSql)
                Console.WriteLine($"  [{m.ID}] {m.Titulo} ({m.Ano}) | {m.Lingua} | {m.Classificacao}/5 | {NomeCategoria(m.CategoriaId)} | {NomeRealizador(m.RealizadorId)}");

            if (pausar) Pausa();
        }

        private void PesquisarFilme()
        {
            Console.Write("Titulo a pesquisar: ");
            string titulo = Console.ReadLine() ?? "";

            var categorias = _categorySQLService.Listar();
            var realizadores = _directorSQLService.Listar();

            string NomeCategoria(int id) => categorias.Find(c => c.ID == id)?.Nome ?? $"[ID {id}]";
            string NomeRealizador(int id) => realizadores.Find(d => d.ID == id)?.Nome ?? $"[ID {id}]";

            void MostrarFilme(Movie? m, string fonte)
            {
                if (m == null)
                    Console.WriteLine($"  ({fonte}) Nao encontrado.");
                else
                    Console.WriteLine($"  ({fonte}) [{m.ID}] {m.Titulo} ({m.Ano}) | {m.Lingua} | {m.Classificacao}/5 | {NomeCategoria(m.CategoriaId)} | {NomeRealizador(m.RealizadorId)}");
            }

            MostrarFilme(_movieService.ObterPorTitulo(titulo), "Memoria");
            MostrarFilme(_movieSQLService.ObterPorTitulo(titulo), "SQLite");
            Pausa();
        }

        private void RemoverFilme()
        {
            int id = LerInteiro("ID do filme a remover: ");
            try { _movieService.Remover(id); Console.WriteLine("Removido da memoria."); }
            catch (Exception ex) { Console.WriteLine($"Memoria: {ex.Message}"); }

            try { _movieSQLService.Remover(id); Console.WriteLine("Removido do SQLite."); }
            catch (Exception ex) { Console.WriteLine($"SQLite: {ex.Message}"); }
            Pausa();
        }

        //  ASSISTENTES
        private static int LerInteiro(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int valor))
                    return valor;
                Console.WriteLine("Valor invalido, introduz um numero inteiro.");
            }
        }

        private static int LerInteiroEntre(string prompt, int min, int max)
        {
            while (true)
            {
                int valor = LerInteiro(prompt);
                if (valor >= min && valor <= max) return valor;
                Console.WriteLine($"O valor deve ser entre {min} e {max}.");
            }
        }

        private static void Pausa()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}