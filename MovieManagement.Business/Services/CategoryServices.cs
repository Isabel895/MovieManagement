using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Business.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _repository;

        public CategoryServices(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new Exception("O nome da categoria nao pode estar vazio");

            if (_repository.ExistePorNome(nome))
                throw new Exception("Ja existe uma categoria com esse nome");

            Category novo = new Category { Nome = nome };
            _repository.Adicionar(novo);
        }

        public List<Category> Listar()
        {
            return _repository.Listar();
        }

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Categoria nao encontrada");
        }
    }
}