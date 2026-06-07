using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Business.Services
{
    public class DirectorServices : IDirectorServices
    {
        private readonly IDirectorRepository _repository;

        public DirectorServices(IDirectorRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(string nome, string pais)
        {
            if (string.IsNullOrEmpty(nome))
                throw new Exception("O nome do realizador nao pode estar vazio");

            if (_repository.ExistePorNome(nome))
                throw new Exception("Ja existe um realizador com esse nome");

            Director novo = new Director { Nome = nome, Pais = pais };
            _repository.Adicionar(novo);
        }

        public List<Director> Listar()
        {
            return _repository.Listar();
        }

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Realizador nao encontrado");
        }
    }
}