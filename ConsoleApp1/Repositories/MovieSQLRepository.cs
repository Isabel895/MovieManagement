using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.PortableExecutable;


namespace MovieManagement.Data.Repositories
{
    public class MovieSQLRepository : IMovieRepository
    {
        private string _connectionString = "Data source = movies.db";//string de conexão à db

        // criação da tabela

        public MovieSQLRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Movies (ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Título TEXT NOT NULL,
Ano INTEGER NOT NULL,
Língua TEXT NOT NULL,
Classificação INTEGER NOT NULL
);";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }


        //implementação dos métodos da interface
        public void Adicionar(Movie movie)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Movies (Título,Ano,Língua,Classificação) Values(@t,@a,@l,@c)";


            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@t", movie.Titulo);
            cmd.Parameters.AddWithValue("@a", movie.Ano);
            cmd.Parameters.AddWithValue("@l", movie.Lingua);
            cmd.Parameters.AddWithValue("@c", movie.Classificacao);

            cmd.ExecuteNonQuery();

        }

        public List<Movie> Listar()
        {
            List<Movie> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT ID,Título,Ano,Língua,Classificação FROM Movies";


            using var cmd = new SqliteCommand(sql, con);

            using var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                lista.Add(new Movie
                {
                    ID = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4),
                });
            }
            return lista;
        }

        public Movie? ObterPorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT ID,Título,Ano,Língua,Classificação FROM Movies WHERE Título=@t";


            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", titulo);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Movie
                {
                    ID = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4),
                };

            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Movies WHERE ID=@id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;

        }

        public bool ExistePorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT COUNT(*) FROM Movies WHERE Título=@t";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", titulo);

            long count = (long)cmd.ExecuteScalar();

            return count > 0;
        }
    }
}
