using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Npgsql;
namespace CampusLove.Domain.Interfaces 
{
    public class PgsqlUsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public PgsqlUsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Users Users)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = @"INSERT INTO Users (first_name, last_name, email, password, birth_date, id_gender, id_career, id_address, profile_phrase)
                          VALUES (@first_name, @last_name , @email , @password, @birth_date, @id_gender, @id_career, @id_address, @profile_phrase)";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@first_name", Users.first_name);
            command.Parameters.AddWithValue("@last_name", Users.last_name); // Corregido espacio extra
            command.Parameters.AddWithValue("@email", Users.email); // Corregido espacio extra
            command.Parameters.AddWithValue("@password", Users.password);
            command.Parameters.AddWithValue("@birth_date", Users.birth_date);
            command.Parameters.AddWithValue("@id_gender", Users.id_gender);
            command.Parameters.AddWithValue("@id_career", Users.id_career);
            command.Parameters.AddWithValue("@id_address", Users.id_address);
            command.Parameters.AddWithValue("@profile_phrase", Users.profile_phrase);
            command.ExecuteNonQuery();
        }

        public Users? GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id_user", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Users
                {
                    id_user = reader.GetInt32(reader.GetOrdinal("id_user")),
                    first_name = reader.GetString(reader.GetOrdinal("first_name")),
                    last_name  = reader.GetString(reader.GetOrdinal("last_name")),
                    email  = reader.GetString(reader.GetOrdinal("email")),
                    password = reader.GetString(reader.GetOrdinal("password")),
                    birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")),
                    id_gender = reader.GetInt32(reader.GetOrdinal("id_gender")),
                    id_career = reader.GetInt32(reader.GetOrdinal("id_career")),
                    id_address = reader.GetInt32(reader.GetOrdinal("id_address")),
                    profile_phrase = reader.GetString(reader.GetOrdinal("profile_phrase")),
                };
            }
            return null;
        }

        public IEnumerable<Users> GetAll()
        {
            var users = new List<Users>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new Users
                {
                    id_user = reader.GetInt32(reader.GetOrdinal("id_user")),
                    first_name = reader.GetString(reader.GetOrdinal("first_name")),
                    last_name  = reader.GetString(reader.GetOrdinal("last_name")),
                    email  = reader.GetString(reader.GetOrdinal("email")),
                    password = reader.GetString(reader.GetOrdinal("password")),
                    birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")),
                    id_gender = reader.GetInt32(reader.GetOrdinal("id_gender")),
                    id_career = reader.GetInt32(reader.GetOrdinal("id_career")),
                    id_address = reader.GetInt32(reader.GetOrdinal("id_address")),
                    profile_phrase = reader.GetString(reader.GetOrdinal("profile_phrase")),
                });
            }
            return users;
        }

        public void Update(Users user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = @"UPDATE Users SET first_name = @first_name, last_name = @last_name , email = @email , password = @password,
                          birth_date = @birth_date, id_gender = @id_gender, id_career = @id_career, id_address = @id_address,
                          profile_phrase = @profile_phrase WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@first_name", user.first_name);
            command.Parameters.AddWithValue("@last_name", user.last_name); // Corregido espacio extra
            command.Parameters.AddWithValue("@email", user.email); // Corregido espacio extra
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@birth_date", user.birth_date);
            command.Parameters.AddWithValue("@id_gender", user.id_gender);
            command.Parameters.AddWithValue("@id_career", user.id_career);
            command.Parameters.AddWithValue("@id_address", user.id_address);
            command.Parameters.AddWithValue("@profile_phrase", user.profile_phrase);
            command.Parameters.AddWithValue("@id_user", user.id_user);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "DELETE FROM Users WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id_user", id);
            command.ExecuteNonQuery();
        }

       public Users? GetByEmail(string email)
{
    using var connection = new NpgsqlConnection(_connectionString);
    connection.Open();
    var query = "SELECT * FROM Users WHERE email = @Email";
    using var command = new NpgsqlCommand(query, connection);
    command.Parameters.AddWithValue("@Email", email);
    using var reader = command.ExecuteReader();
    if (reader.Read())
    {
        return new Users
        {
            id_user = reader.GetInt32(reader.GetOrdinal("id_user")),
            first_name = reader.GetString(reader.GetOrdinal("first_name")),
            last_name = reader.GetString(reader.GetOrdinal("last_name")),
            email = reader.GetString(reader.GetOrdinal("email")),
            password = reader.GetString(reader.GetOrdinal("password")),
            birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")),
            id_gender = reader.GetInt32(reader.GetOrdinal("id_gender")),
            id_career = reader.GetInt32(reader.GetOrdinal("id_career")),
            id_address = reader.GetInt32(reader.GetOrdinal("id_address")),
            profile_phrase = reader.GetString(reader.GetOrdinal("profile_phrase")),
        };
    }
    return null;
}

    }
}
