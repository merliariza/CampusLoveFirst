using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlUsersInterestsRepository : IUsersInterestsRepository
    {
        private readonly string _connStr;

        public PgsqlUsersInterestsRepository(string connStr)
        {
            _connStr = connStr;
        }

        public void AddUserInterest(int id_user, int id_interest)
        {
            using var conn = new NpgsqlConnection(_connStr);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO public.usersinterests (id_user, id_interest) VALUES (@user, @interest)", conn);
            cmd.Parameters.AddWithValue("@user", id_user);
            cmd.Parameters.AddWithValue("@interest", id_interest);
            cmd.ExecuteNonQuery();
        }

        public void AddUserInterests(int id_user, IEnumerable<int> interests)
        {
            using var conn = new NpgsqlConnection(_connStr);
            conn.Open();
            foreach (var interest in interests)
            {
                using var cmd = new NpgsqlCommand("INSERT INTO UsersInterests(id_user, id_interest) VALUES (@user, @interest)", conn);
                cmd.Parameters.AddWithValue("@user", id_user);
                cmd.Parameters.AddWithValue("@interest", interest);
                cmd.ExecuteNonQuery();
            }
        }
    }
}