using MovieManagement.Domain.Entities;

namespace MovieManagement.Business.Services
{
    public interface ICategoryServices
    {
        void Adicionar(string nome);
        List<Category> Listar();
        void Remover(int id);
    }
}