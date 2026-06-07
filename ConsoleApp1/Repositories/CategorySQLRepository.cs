using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class CategorySQLRepository : ICategoryRepository
    {
        private string _connectionString = "Data Source=movies.db";

        public CategorySQLRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Categories (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Category category)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "INSERT INTO Categories (Nome) VALUES(@n)";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", category.Nome);
            cmd.ExecuteNonQuery();
        }

        public List<Category> Listar()
        {
            List<Category> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Nome FROM Categories";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Category
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                });
            }
            return lista;
        }

        public Category? ObterPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT ID, Nome FROM Categories WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Category
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                };
            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "DELETE FROM Categories WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;
        }

        public bool ExistePorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM Categories WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public bool ExistePorId(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM Categories WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }
    }
}
