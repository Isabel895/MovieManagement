using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class MovieSQLRepository : IMovieRepository
    {
        private string _connectionString = "Data Source=movies.db";

        public MovieSQLRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();
            AtivarForeignKeys(con);

            string sql = @"CREATE TABLE IF NOT EXISTS Movies (
                ID           INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo       TEXT    NOT NULL,
                Ano          INTEGER NOT NULL,
                Lingua       TEXT    NOT NULL,
                Classificacao INTEGER NOT NULL,
                CategoriaId  INTEGER NOT NULL,
                RealizadorId INTEGER NOT NULL,
                FOREIGN KEY (CategoriaId)  REFERENCES Categories(ID),
                FOREIGN KEY (RealizadorId) REFERENCES Directors(ID)
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Movie movie)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();
            AtivarForeignKeys(con);

            string sql = "INSERT INTO Movies (Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId) VALUES(@t, @a, @l, @c, @cat, @rea)";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", movie.Titulo);
            cmd.Parameters.AddWithValue("@a", movie.Ano);
            cmd.Parameters.AddWithValue("@l", movie.Lingua);
            cmd.Parameters.AddWithValue("@c", movie.Classificacao);
            cmd.Parameters.AddWithValue("@cat", movie.CategoriaId);
            cmd.Parameters.AddWithValue("@rea", movie.RealizadorId);
            cmd.ExecuteNonQuery();
        }

        public void Editar(Movie movie)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();
            AtivarForeignKeys(con);

            string sql = "UPDATE Movies SET Titulo=@t, Ano=@a, Lingua=@l, Classificacao=@c, CategoriaId=@cat, RealizadorId=@rea WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", movie.Titulo);
            cmd.Parameters.AddWithValue("@a", movie.Ano);
            cmd.Parameters.AddWithValue("@l", movie.Lingua);
            cmd.Parameters.AddWithValue("@c", movie.Classificacao);
            cmd.Parameters.AddWithValue("@cat", movie.CategoriaId);
            cmd.Parameters.AddWithValue("@rea", movie.RealizadorId);
            cmd.Parameters.AddWithValue("@id", movie.ID);
            cmd.ExecuteNonQuery();
        }

        public List<Movie> Listar()
        {
            List<Movie> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Movies";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                lista.Add(Mapear(reader));

            return lista;
        }

        public Movie? ObterPorId(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Movies WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            return reader.Read() ? Mapear(reader) : null;
        }

        public Movie? ObterPorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Movies WHERE Titulo=@t";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", titulo);
            using var reader = cmd.ExecuteReader();

            return reader.Read() ? Mapear(reader) : null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();
            AtivarForeignKeys(con);

            string sql = "DELETE FROM Movies WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExistePorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM Movies WHERE Titulo=@t";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", titulo);

            return (long)cmd.ExecuteScalar()! > 0;
        }

        private static void AtivarForeignKeys(SqliteConnection con)
        {
            using var cmd = new SqliteCommand("PRAGMA foreign_keys = ON;", con);
            cmd.ExecuteNonQuery();
        }

        private static Movie Mapear(SqliteDataReader r) => new Movie
        {
            ID = r.GetInt32(0),
            Titulo = r.GetString(1),
            Ano = r.GetInt32(2),
            Lingua = r.GetString(3),
            Classificacao = r.GetInt32(4),
            CategoriaId = r.GetInt32(5),
            RealizadorId = r.GetInt32(6),
        };
    }
}