using System.Collections.Generic;
using System.Data;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Domain.Interfaces 
{
    public class PgsqlGendersRepository : IGendersRepository
    {
        private readonly string _connectionString;

        public PgsqlGendersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Genders> GetAll()
        {
            var list = new List<Genders>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_gender, genre_name FROM genders", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Genders
                {
                    id_gender = reader.GetInt32(0),
                    genre_name = reader.GetString(1)
                });
            }

            return list;
        }

        public Genders GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_gender, genre_name FROM genders WHERE id_gender = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Genders
                {
                    id_gender = reader.GetInt32(0),
                    genre_name = reader.GetString(1)
                };
            }

            return null;
        }
    }
}
