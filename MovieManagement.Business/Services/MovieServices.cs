using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Business.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDirectorRepository _directorRepository;

        private static readonly int AnoMinimo = 1888;
        private static readonly int AnoMaximo = DateTime.Now.Year;

        public MovieServices(IMovieRepository repository, ICategoryRepository categoryRepository, IDirectorRepository directorRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _directorRepository = directorRepository;
        }

        public void Adicionar(string titulo, int ano, string lingua, int classificacao, int categoriaId, int realizadorId)
        {
            ValidarCampos(titulo, ano, classificacao, categoriaId, realizadorId);

            if (_repository.ExistePorTitulo(titulo))
                throw new Exception("Ja existe um filme com esse titulo");

            Movie novo = new Movie
            {
                Titulo = titulo,
                Ano = ano,
                Lingua = lingua,
                Classificacao = classificacao,
                CategoriaId = categoriaId,
                RealizadorId = realizadorId
            };
            _repository.Adicionar(novo);
        }

        public void Editar(int id, string titulo, int ano, string lingua, int classificacao, int categoriaId, int realizadorId)
        {
            Movie? existente = _repository.ObterPorId(id);
            if (existente == null)
                throw new Exception("Filme nao encontrado");

            ValidarCampos(titulo, ano, classificacao, categoriaId, realizadorId);

            if (!existente.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase) && _repository.ExistePorTitulo(titulo))
                throw new Exception("Ja existe um filme com esse titulo");

            existente.Titulo = titulo;
            existente.Ano = ano;
            existente.Lingua = lingua;
            existente.Classificacao = classificacao;
            existente.CategoriaId = categoriaId;
            existente.RealizadorId = realizadorId;

            _repository.Editar(existente);
        }

        public List<Movie> Listar() => _repository.Listar();

        public List<Movie> ListarOrdenado(string criterio)
        {
            var filmes = _repository.Listar();
            return criterio switch
            {
                "titulo" => filmes.OrderBy(m => m.Titulo).ToList(),
                "ano" => filmes.OrderBy(m => m.Ano).ToList(),
                "classificacao" => filmes.OrderByDescending(m => m.Classificacao).ToList(),
                _ => filmes
            };
        }

        public List<Movie> FiltrarPorCategoria(int categoriaId) =>
            _repository.Listar().Where(m => m.CategoriaId == categoriaId).ToList();

        public List<Movie> FiltrarPorRealizador(int realizadorId) =>
            _repository.Listar().Where(m => m.RealizadorId == realizadorId).ToList();

        public Movie? ObterPorTitulo(string titulo) => _repository.ObterPorTitulo(titulo);

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Filme nao encontrado");
        }

        private void ValidarCampos(string titulo, int ano, int classificacao, int categoriaId, int realizadorId)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new Exception("O titulo nao pode estar vazio");

            if (ano < AnoMinimo || ano > AnoMaximo)
                throw new Exception($"O ano deve ser entre {AnoMinimo} e {AnoMaximo}");

            if (classificacao < 0 || classificacao > 5)
                throw new Exception("A classificacao deve ser entre 0 e 5");

            if (!_categoryRepository.ExistePorId(categoriaId))
                throw new Exception("A categoria especificada nao existe");

            if (!_directorRepository.ExistePorId(realizadorId))
                throw new Exception("O realizador especificado nao existe");
        }
    }
}