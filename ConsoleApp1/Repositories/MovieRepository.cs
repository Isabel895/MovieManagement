using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> _movies;
        private int _proximoID;

        public MovieRepository()
        {
            _movies = new List<Movie>();
            _proximoID = 1;
        }

        public void Adicionar(Movie movie)
        {
            movie.ID = _proximoID++;
            _movies.Add(movie);
        }

        public void Editar(Movie movie)
        {
            Movie? existente = _movies.FirstOrDefault(m => m.ID == movie.ID);
            if (existente == null) return;

            existente.Titulo = movie.Titulo;
            existente.Ano = movie.Ano;
            existente.Lingua = movie.Lingua;
            existente.Classificacao = movie.Classificacao;
            existente.CategoriaId = movie.CategoriaId;
            existente.RealizadorId = movie.RealizadorId;
        }

        public List<Movie> Listar() => _movies;

        public Movie? ObterPorId(int id) => _movies.FirstOrDefault(m => m.ID == id);

        public Movie? ObterPorTitulo(string titulo) =>
            _movies.FirstOrDefault(m => m.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        public bool Remover(int id)
        {
            Movie? movie = _movies.FirstOrDefault(m => m.ID == id);
            if (movie == null) return false;
            _movies.Remove(movie);
            return true;
        }

        public bool ExistePorTitulo(string titulo) =>
            _movies.Any(m => m.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
    }
}