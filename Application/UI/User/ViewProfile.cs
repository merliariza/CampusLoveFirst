using System;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;
using CampusLove.Application.Utils;
namespace CampusLove.Application.UI.User
{
    public class ViewMyProfile
    {
        private readonly UserService _userService;
        private readonly UsersInterestsService _usersInterestsService;
        private readonly InterestsService _interestsService;
        private readonly GendersService _gendersService;
        private readonly CareersService _careersService;
        private readonly AddressesService _addressesService;
        private readonly dynamic _currentUser;

        public ViewMyProfile(UserService userService,
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

       public void ShowMyProfile()
        {
            Console.Clear();

            var user = _userService.ObtenerPorId(_currentUser.id_user);
            if (user == null)
            {
                Console.WriteLine("❌ Error: No se pudo cargar tu perfil.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // El resto queda igual...
            var gender = _gendersService.GetById(user.id_gender)?.genre_name ?? "No especificado";
            var career = _careersService.GetById(user.id_career)?.career_name ?? "No especificado";
            var address = _addressesService.GetFullAddress(user.id_address);
            var userInterests = (IEnumerable<UsersInterests>)_usersInterestsService.GetUserInterests(user.id_user);
            var interests = userInterests
                .Select(ui => _interestsService.GetById(ui.id_interest)?.interest_name)
                .Where(i => i != null);

            // Mostrar perfil...
            Console.WriteLine("♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥");
            Console.WriteLine("\nTU PERFIL");
            Console.WriteLine("\n♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥");

            Console.WriteLine($"\n👤 Nombre: {user.first_name} {user.last_name}");
            Console.WriteLine($"🎂 Edad: {CalculateAge(user.birth_date)} años");
            Console.WriteLine($"📧 Email: {user.email}");
            Console.WriteLine($"🚻 Género: {gender}");
            Console.WriteLine($"🎓 Carrera: {career}");
            Console.WriteLine($"\n💬 Frase de perfil: \"{user.profile_phrase}\"");
            Console.WriteLine($"\n🏠 Ubicación: {address}");
            Console.WriteLine("\n❤️ Intereses:");
            foreach (var interest in interests)
            {
                Console.WriteLine($"- {interest}");
            }

            Console.WriteLine("\n♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥");
            Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
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