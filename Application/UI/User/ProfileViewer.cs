using System;
using System.Linq;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class ProfileViewer
    {
        private readonly UserService _userService;
        private readonly UsersInterestsService _usersInterestsService;
        private readonly InterestsService _interestsService;
        private readonly GendersService _gendersService;
        private readonly CareersService _careersService;
        private readonly AddressesService _addressesService;
        private readonly dynamic _currentUser;

        public ProfileViewer(UserService userService,
                             UsersInterestsService usersInterestsService,
                             InterestsService interestsService,
                             GendersService gendersService,
                             CareersService careersService,
                             AddressesService addressesService,
                             dynamic currentUser)
        {
            _userService = userService;
            _usersInterestsService = usersInterestsService;
            _interestsService = interestsService;
            _gendersService = gendersService;
            _careersService = careersService;
            _addressesService = addressesService;
            _currentUser = currentUser;
        }

        public void BrowseProfiles()
        {
            Console.Clear();
            var allUsers = _userService.ObtenerTodos();
            var otherUsers = allUsers
                .Where(u => u.id_user != _currentUser.id_user)
                .ToList();

            if (!otherUsers.Any())
            {
                Console.WriteLine("❌ No hay perfiles para mostrar.");
                return;
            }

            int index = 0;

            while (true)
            {
                Console.Clear();
                var user = otherUsers[index];
                ShowProfile(user);

                Console.WriteLine(@"
¿Qué deseas hacer?
    [L] Like
    [D] Dislike
    [N] Siguiente
    [P] Anterior
    [S] Salir
");
                var option = Console.ReadLine()?.Trim().ToUpper();

                switch (option)
                {
                    case "L":
                        Console.WriteLine($"💖 Diste like a {user.first_name}.");
                        break;
                    case "D":
                        Console.WriteLine($"👎 Diste dislike a {user.first_name}.");
                        break;
                    case "N":
                        index = (index + 1) % otherUsers.Count;
                        break;
                    case "P":
                        index = (index - 1 + otherUsers.Count) % otherUsers.Count;
                        break;
                    case "S":
                        return;
                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }

                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private void ShowProfile(CampusLove.Domain.Entities.Users user)
        {
            var gender = _gendersService.GetById(user.id_gender)?.genre_name ?? "No especificado";
            var career = _careersService.GetById(user.id_career)?.career_name ?? "No especificado";
            var address = _addressesService.GetFullAddress(user.id_address);

            var userInterests = (IEnumerable<UsersInterests>)_usersInterestsService.GetUserInterests(user.id_user);
            var interests = userInterests
                .Select(ui => _interestsService.GetById(ui.id_interest)?.interest_name)
                .Where(i => i != null);

            string interestsList = string.Join(Environment.NewLine, interests.Select(i => "                        - " + i));

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.WriteLine($@"
                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
                ███████████████████████████████████████████████████████████████████
                ███─▄▄▄─██▀▄─██▄─▀█▀─▄█▄─▄▄─█▄─██─▄█─▄▄▄▄█▄─▄███─▄▄─█▄─█─▄█▄─▄▄─███
                ███─███▀██─▀─███─█▄█─███─▄▄▄██─██─██▄▄▄▄─██─██▀█─██─██▄▀▄███─▄█▀███
                ▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▀▀

                    👤 Nombre: {user.first_name} {user.last_name}
                    🎂 Edad: {CalculateAge(user.birth_date)} años
                    📧 Email: {user.email}
                    🚻 Género: {gender}
                    🎓 Carrera: {career}
                    💬 Frase de perfil: ""{user.profile_phrase}""
                    🏠 Ubicación: {address}

                    ❤️ Intereses:
{interestsList}

                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
