using MovieManagement.Business.Services;
using MovieManagement.Data.Repositories;

namespace MovieManagement.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Repositórios em memória
            var categoryMemRepo = new CategoryRepository();
            var directorMemRepo = new DirectorRepository();
            var movieMemRepo = new MovieRepository();

            // Repositórios SQLite
            var categorySQLRepo = new CategorySQLRepository();
            var directorSQLRepo = new DirectorSQLRepository();
            var movieSQLRepo = new MovieSQLRepository();

            // Serviços
            var categoryService = new CategoryServices(categoryMemRepo);
            var categorySQLService = new CategoryServices(categorySQLRepo);
            var directorService = new DirectorServices(directorMemRepo);
            var directorSQLService = new DirectorServices(directorSQLRepo);
            var movieService = new MovieServices(movieMemRepo, categoryMemRepo, directorMemRepo);
            var movieSQLService = new MovieServices(movieSQLRepo, categorySQLRepo, directorSQLRepo);

            // Arrancar UI
            var menu = new MenuUI(
                categoryService, categorySQLService,
                directorService, directorSQLService,
                movieService, movieSQLService);

            menu.Executar();
        }
    }
}