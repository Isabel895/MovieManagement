using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repository
{
    public class MovieRepository: IMovieRepository
    {
        private List<Movie> _movies;

        public MovieRepository()
        {
            _movies = new List<Movie>();
        }


    }
}
