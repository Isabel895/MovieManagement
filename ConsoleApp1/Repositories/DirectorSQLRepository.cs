using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class DirectorSQLRepository : IDirectorRepository
    {
        private string _connectionString = "Data Source=movies.db";

        public DirectorSQLRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Directors (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Pais TEXT NOT NULL
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Director director)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "INSERT INTO Directors (Nome, Pais) VALUES(@n, @p)";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", director.Nome);
            cmd.Parameters.AddWithValue("@p", director.Pais);
            cmd.ExecuteNonQuery();
        }

        public List<Director> Listar()
        {
            List<Director> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Nome, Pais FROM Directors";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Director
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2),
                });
            }
            return lista;
        }

        public Director? ObterPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Nome, Pais FROM Directors WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Director
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2),
                };
            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "DELETE FROM Directors WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;
        }

        public bool ExistePorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM Directors WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public bool ExistePorId(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM Directors WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }
    }
}
