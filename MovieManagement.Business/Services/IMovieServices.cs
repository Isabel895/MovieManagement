using MovieManagement.Domain.Entities;

namespace MovieManagement.Business.Services
{
    public interface IMovieServices
    {
        void Adicionar(string titulo, int ano, string lingua, int classificacao, int categoriaId, int realizadorId);
        void Editar(int id, string titulo, int ano, string lingua, int classificacao, int categoriaId, int realizadorId);
        List<Movie> Listar();
        Movie? ObterPorTitulo(string titulo);
        void Remover(int id);
    }
}