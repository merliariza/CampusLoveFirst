using System;
using System.Linq;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

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

        public string ProfileTitle()
        {
            return "";
        }

        public string GetMyProfileString()
{
    Console.Clear();
    var user = _userService.ObtenerPorId(_currentUser.id_user);
    if (user == null)
    {
        return
@"❌ Error: No se pudo cargar tu perfil.";
    }

    var gender = _gendersService.GetById(user.id_gender)?.genre_name ?? "No especificado";
    var career = _careersService.GetById(user.id_career)?.career_name ?? "No especificado";
    var address = _addressesService.GetFullAddress(user.id_address);

    var creditosDisponibles = _userService.ObtenerCreditosDisponibles(user.id_user);

    var userInterests = (IEnumerable<UsersInterests>)_usersInterestsService.GetUserInterests(user.id_user);
    var interests = userInterests
        .Select(ui => _interestsService.GetById(ui.id_interest)?.interest_name)
        .Where(i => i != null);
    string interestsList = string.Join(Environment.NewLine,
        interests.Select(i => "                        - " + i));

    Console.InputEncoding = System.Text.Encoding.UTF8;
    Console.Clear();
    return
    $@"
        ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
        ███████████████████████████████████████████████████████████████████
        ███─▄▄▄─██▀▄─██▄─▀█▀─▄█▄─▄▄─█▄─██─▄█─▄▄▄▄█▄─▄███─▄▄─█▄─█─▄█▄─▄▄─███
        ███─███▀██─▀─███─█▄█─███─▄▄▄██─██─██▄▄▄▄─██─██▀█─██─██▄▀▄███─▄█▀███
        ▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▀▀
                                
                                𝚃𝚞 𝚒𝚗𝚏𝚘𝚛𝚖𝚊𝚌𝚒𝚘́𝚗 𝚙𝚎𝚛𝚜𝚘𝚗𝚊𝚕

            👤 Nombre: {user.first_name} {user.last_name}
            🎂 Edad: {CalculateAge(user.birth_date)} años
            📧 Email: {user.email}
            🚻 Género: {gender}
            🎓 Carrera: {career}

            💬 Frase de perfil: ""{user.profile_phrase}""

            🏠 Ubicación: {address}

            💰 Créditos disponibles hoy: {creditosDisponibles}

            ❤️ Intereses:
{interestsList}

        ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
";
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
