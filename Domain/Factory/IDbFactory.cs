using CampusLove.Domain.Interfaces;

namespace CampusLove.Domain.Interfaces
{
    public interface IDbfactory
    {
        IUsersRepository CreateUsersRepository();
        IAddressesRepository CreateAddressesRepository();
        ICareersRepository CreateCareersRepository();
        IGendersRepository CreateGendersRepository();
        IInterestsRepository CreateInterestsRepository();
        IUsersInterestsRepository CreateUsersInterestsRepository();
        IInteractionsRepository CreateInteractionsRepository();
        IInteractionsCreditsRepository CreateInteractionCreditsRepository();
        IMatchesRepository CreateMatchesRepository();
    }
}