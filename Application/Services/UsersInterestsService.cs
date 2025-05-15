using CampusLove.Domain.Interfaces;
using System.Collections.Generic;

namespace CampusLove.Application.Services
{
    public class UsersInterestsService
    {
        private readonly IUsersInterestsRepository _repository;

        public UsersInterestsService(IUsersInterestsRepository repository)
        {
            _repository = repository;
        }

        public void AsociarIntereses(int id_user, IEnumerable<int> intereses)
        {
            _repository.AddUserInterests(id_user, intereses);
        }

        public void Guardar(int userId, int interesId)
        {
            _repository.AddUserInterest(userId, interesId);
        }
    }
}
