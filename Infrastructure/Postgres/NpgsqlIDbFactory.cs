using CampusLove.Infrastructure.Repositories;

namespace CampusLove.Domain.Interfaces
{
    public class NpgsqlDbFactory : IDbfactory
    {
        private readonly string _connectionString;

        public NpgsqlDbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new PgsqlUsersRepository(_connectionString);
        }

        public IGendersRepository CreateGendersRepository()
        {
            return new PgsqlGendersRepository(_connectionString);
        }

        public ICareersRepository CreateCareersRepository()
        {
            return new PgsqlCareersRepository(_connectionString);
        }

        public IAddressesRepository CreateAddressesRepository()
        {
            return new PgsqlAddressesRepository(_connectionString);
        }
        public IInterestsRepository CreateInterestsRepository()
        {
            return new PgsqlInterestsRepository(_connectionString);
        }
        public IUsersInterestsRepository CreateUsersInterestsRepository()
        {
            return new PgsqlUsersInterestsRepository(_connectionString);
        }
        public IInteractionsRepository CreateInteractionsRepository()
        {
            return new PgsqlInteractionsRepository(_connectionString);
        }
        public IInteractionsCreditsRepository CreateInteractionCreditsRepository()
        {
            return new PgsqlInteractionCreditsRepository(_connectionString);
        }
        public IMatchesRepository CreateMatchesRepository()
        {
            return new PgsqlMatchesRepository(_connectionString);
        }
    }
}
