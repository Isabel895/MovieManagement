using MovieManagement.Domain.Entities;

namespace MovieManagement.Business.Services
{
    public interface IDirectorServices
    {
        void Adicionar(string nome, string pais);
        List<Director> Listar();
        void Remover(int id);
    }
}