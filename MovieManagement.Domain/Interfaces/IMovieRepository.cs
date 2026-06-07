using MovieManagement.Domain.Entities;

namespace MovieManagement.Domain.Interfaces
{
    public interface IMovieRepository
    {
        void Adicionar(Movie movie);
        void Editar(Movie movie);
        List<Movie> Listar();
        Movie? ObterPorId(int id);
        Movie? ObterPorTitulo(string titulo);
        bool Remover(int id);
        bool ExistePorTitulo(string titulo);
    }
}