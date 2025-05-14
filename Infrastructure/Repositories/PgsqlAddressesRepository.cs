using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Infrastructure.Repositories; 

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlAddressesRepository : IAddressesRepository
    {
        private readonly string _connectionString;

        public PgsqlAddressesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Addresses> GetAll()
        {
            var list = new List<Addresses>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city FROM Addresses", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1)
                });
            }

            return list;
        }

        public Addresses GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city FROM Addresses WHERE id_address = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1)
                };
            }

            return null;
        }
        public Addresses Create(Addresses newAddress)

        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("INSERT INTO addresses (id_city) VALUES (@id_city) RETURNING id_address", connection);
            command.Parameters.AddWithValue("@id_city", newAddress.id_city);

            var result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                newAddress.id_address = id;
                return newAddress;
            }

            throw new Exception("No se pudo insertar la dirección.");
        }


        public IEnumerable<Countries> GetAllCountries()
        {
            var countries = new List<Countries>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_country, name_country FROM countries", connection); 
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                countries.Add(new Countries
                {
                    id_country = reader.GetInt32(0),
                    name_country = reader.GetString(1)
                });
            }

            return countries;
        }


        public IEnumerable<States> GetStatesByCountry(int id_country)
        {
            var states = new List<States>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_state, state_name FROM states WHERE id_country = @id_country", connection);
            command.Parameters.AddWithValue("@id_country", id_country);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                states.Add(new States
                {
                    id_state = reader.GetInt32(0),
                    state_name = reader.GetString(1)
                });
            }

            return states;
        }

        public IEnumerable<Cities> GetCitiesByState(int id_state)
        {
            var cities = new List<Cities>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_city, city_name FROM cities WHERE id_state = @id_state", connection);
            command.Parameters.AddWithValue("@id_state", id_state);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                cities.Add(new Cities
                {
                    id_city = reader.GetInt32(0),
                    city_name = reader.GetString(1)
                });
            }

            return cities;
        }

        public Addresses BuscarDireccion(int cityId, string street_number, string street_name)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city FROM addresses WHERE id_city = @cityId AND street_number = @street_number AND street_name = @street_name", connection);
            command.Parameters.AddWithValue("@cityId", cityId);
            command.Parameters.AddWithValue("@street_number", street_number);
            command.Parameters.AddWithValue("@street_name", street_name);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1)
                };
            }
            return null;
        }

    }
}
