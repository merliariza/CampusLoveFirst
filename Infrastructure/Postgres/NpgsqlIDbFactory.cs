using System;
using CampusLove.domain.Factory;
namespace CampusLove.Infrastructure.Pgsql;

public class NpgsqlgDbFactory : IDbfactory
{
    private readonly string _connectionString;

    public NpgsqlgDbFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
}