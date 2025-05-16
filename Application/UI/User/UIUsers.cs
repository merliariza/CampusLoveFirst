using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class UIUsers
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
        private readonly dynamic _usuario;

                
        public UIUsers(
            UserService userService,
            UsersInterestsService usersInterestsService,
            InterestsService interestsService,
            GendersService gendersService,
            CareersService careersService,
            AddressesService addressesService,
            InteractionsService interactionsService,
            InteractionCreditsService creditsService,
            MatchesService matchesService,
            dynamic usuario)
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
            _usuario = usuario;
        }

        public string InitialMenu()
        {
            Console.Clear();
            var creditos = _creditsService.GetAvailableCredits((int)_usuario.id_user);

            return @$"
                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
                    ███████████████████████████████████████████████████████████████████
                    ███─▄▄▄─██▀▄─██▄─▀█▀─▄█▄─▄▄─█▄─██─▄█─▄▄▄▄█▄─▄███─▄▄─█▄─█─▄█▄─▄▄─███
                    ███─███▀██─▀─███─█▄█─███─▄▄▄██─██─██▄▄▄▄─██─██▀█─██─██▄▀▄███─▄█▀███
                    ▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▀▀
                                              
                            Bienvenido, {_usuario.first_name} {_usuario.last_name}!  
                            Créditos disponibles hoy: {creditos}
                            
                            1. Ver perfiles y dar Like o Dislike      
                            2. Ver mis coincidencias (Matches)        
                            3. Ver estadísticas del sistema           
                            4. Ver mi perfil                          
                            0. Cerrar sesión    
                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥";
        }

        public void Ejecutar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(InitialMenu());
                Console.Write("Seleccione una opción: ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        var viewer = new ProfileViewer(
                        _userService,
                        _usersInterestsService,
                        _interestsService,
                        _gendersService,
                        _careersService,
                        _addressesService,
                        _interactionsService,
                        _creditsService,
                        _matchesService,
                        _usuario);
                        viewer.BrowseProfiles();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("♥♥♥♥♥♥ MIS COINCIDENCIAS ♥♥♥♥♥♥");
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("♥♥♥♥♥♥ ESTADÍSTICAS DEL SISTEMA ♥♥♥♥♥♥");
                        break;

                    case "4":
                        Console.Clear();
                        var viewProfile = new ViewMyProfile(
                            _userService,
                            _usersInterestsService,
                            _interestsService,
                            _gendersService,
                            _careersService,
                            _addressesService,
                            _usuario);
                        Console.WriteLine(viewProfile.GetMyProfileString());
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
