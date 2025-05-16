using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class LoginUser
    {
        private readonly UserService _userService;
        private readonly UsersInterestsService _usersInterestsService;
        private readonly InterestsService _interestsService;
        private readonly GendersService _gendersService;
        private readonly CareersService _careersService;
        private readonly AddressesService _addressesService;
       private readonly InteractionsService _interactionsService;
        private readonly InteractionCreditsService _creditsService;
        private readonly MatchesService _matchesService;

        public LoginUser(
            UserService userService,
            UsersInterestsService usersInterestsService,
            InterestsService interestsService,
            GendersService gendersService,
            CareersService careersService,
            AddressesService addressesService,
            InteractionsService interactionsService,
            InteractionCreditsService creditsService,
            MatchesService matchesService)
        {
            _userService = userService;
            _usersInterestsService = usersInterestsService;
            _interestsService = interestsService;
            _gendersService = gendersService;
            _careersService = careersService;
            _addressesService = addressesService;
            _interactionsService = interactionsService;
            _creditsService = creditsService;
            _matchesService = matchesService;
        }

        public void Ejecutar()
        {
            Console.Clear();
            Console.WriteLine("♥♥♥♥♥♥ INICIAR SESIÓN ♥♥♥♥♥♥");

            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim() ?? string.Empty;

            var usuario = _userService.GetByEmail(email);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado. Regístrate primero.");
                Console.ReadKey();
                return;
            }

            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                Console.Write("Contraseña: ");
                var password = Console.ReadLine()?.Trim() ?? string.Empty;

                if (usuario.password == password)
                {
                    Console.Clear();
                    var uiUsers = new UIUsers(
                    _userService,
                    _usersInterestsService,
                    _interestsService,
                    _gendersService,
                    _careersService,
                    _addressesService,
                    _interactionsService,
                    _creditsService,
                    _matchesService,
                    usuario);

                    uiUsers.Ejecutar();
                    return;
                }
                else
                {
                    intentos++;
                    if (intentos < maxIntentos)
                        Console.WriteLine("Contraseña incorrecta. Intenta nuevamente.");
                }
            }

            Console.WriteLine("Has excedido el número de intentos permitidos. Intenta más tarde.");
            Console.ReadKey();
        }
    }
}
