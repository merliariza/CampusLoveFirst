using System;
using System.Linq;

using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class CreateUser
    {
        private readonly UserService _servicio;
        private readonly GendersService _genderService;
        private readonly CareersService _careerService;
        private readonly AddressesService _addressService;

        public CreateUser(UserService servicio, GendersService genderService, CareersService careerService, AddressesService addressService)
        {
            _servicio = servicio;
            _genderService = genderService;
            _careerService = careerService;
            _addressService = addressService;
        }

        public void Ejecutar()
        {
            var user = new Users();
            Console.Clear();
            Console.WriteLine("♥♥♥♥♥♥♥ CREAR USUARIO ♥♥♥♥♥♥♥");

            Console.Write("Nombre: ");
            user.first_name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.first_name) || ContieneNumeros(user.first_name))
            {
                Console.Write(" Nombre inválido (no debe contener números). Ingrese nuevamente: ");
                user.first_name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            Console.Write("Apellido: ");
            user.last_name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.last_name) || ContieneNumeros(user.last_name))
            {
                Console.Write(" Apellido inválido (no debe contener números). Ingrese nuevamente: ");
                user.last_name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            Console.Write("Email: ");
            user.email = Console.ReadLine()?.Trim() ?? string.Empty;

            var (valido, mensaje) = EsEmailValido(user.email);
            while (!valido || _servicio.ExisteEmail(user.email))
            {
                if (!valido)
                {
                    Console.WriteLine($" {mensaje}");
                }
                else if (_servicio.ExisteEmail(user.email))
                {
                    Console.WriteLine(" El email ya está registrado. Intente con otro.");
                }

                Console.Write("Ingrese un nuevo email: ");
                user.email = Console.ReadLine()?.Trim() ?? string.Empty;
                (valido, mensaje) = EsEmailValido(user.email);
            }

            Console.Write("Contraseña: ");
            user.password = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.password) || user.password.Length < 8)
            {
                Console.Write(" Contraseña inválida (mínimo 8 caracteres). Ingrese nuevamente: ");
                user.password = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
            DateTime birthDate;
            while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.Write(" Fecha inválida. Ingrese nuevamente (yyyy-mm-dd): ");
            }
            user.birth_date = birthDate;

            var generos = _genderService.GetAll();
            Console.WriteLine("Seleccione su género:");
            foreach (var g in generos)
                Console.WriteLine($"{g.id_gender}. {g.genre_name}");
            int id_gender;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_gender) && generos.Any(g => g.id_gender == id_gender))
                    break;
                Console.WriteLine(" Opción inválida. Ingrese una opción válida.");
            }
            user.id_gender = id_gender;

            var carreras = _careerService.GetAll();
            Console.WriteLine("Seleccione su carrera:");
            foreach (var c in carreras)
                Console.WriteLine($"{c.id_career}. {c.career_name}");
            int id_career;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_career) && carreras.Any(c => c.id_career == id_career))
                    break;
                Console.WriteLine(" Opción inválida. Ingrese una opción válida.");
            }
            user.id_career = id_career;

            var paises = _addressService.GetAllCountries();
            Console.WriteLine("Seleccione su país:");
            foreach (var p in paises)
                Console.WriteLine($"{p.id_country}. {p.name_country}");

            int id_country;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_country) && paises.Any(p => p.id_country == id_country))
                    break;
                Console.WriteLine(" Opción inválida.");
            }

            var estados = _addressService.GetStatesByCountry(id_country);
            Console.WriteLine("Seleccione su estado:");
            foreach (var s in estados)
                Console.WriteLine($"{s.id_state}. {s.state_name}");

            int id_state;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_state) && estados.Any(s => s.id_state == id_state))
                    break;
                Console.WriteLine(" Opción inválida.");
            }

            var ciudades = _addressService.GetCitiesByState(id_state);
            Console.WriteLine("Seleccione su ciudad:");
            foreach (var c in ciudades)
                Console.WriteLine($"{c.id_city}. {c.city_name}");

            int id_city;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_city) && ciudades.Any(c => c.id_city == id_city))
                    break;
                Console.WriteLine(" Opción inválida.");
            }

            Console.Write("Número de calle: ");
            string street_number = Console.ReadLine()?.Trim().ToUpper() ?? "";
            while (string.IsNullOrWhiteSpace(street_number))
            {
                Console.Write(" Ingrese un número válido: ");
                street_number = Console.ReadLine()?.Trim().ToUpper() ?? "";
            }
            
while (string.IsNullOrWhiteSpace(street_number))
{
    Console.Write(" Ingrese un número válido: ");
    street_number = Console.ReadLine()?.Trim().ToUpper() ?? "";
}


            

            Console.Write("Nombre de la calle: ");
            string street_name = Console.ReadLine()?.Trim().ToUpper() ?? "";
            while (string.IsNullOrWhiteSpace(street_name))
            {
                Console.Write(" Ingrese un nombre válido: ");
                street_name = Console.ReadLine()?.Trim().ToUpper() ?? "";
            }


while (string.IsNullOrWhiteSpace(street_name))
{
    Console.Write(" Ingrese un nombre válido: ");
    street_name = Console.ReadLine()?.Trim().ToUpper() ?? "";
}


            var nuevaDireccion = new Addresses
            {
                id_city = id_city,
                street_number = street_number,
                street_name = street_name
            };

            int id_address = _addressService.ObtenerOCrearDireccion(nuevaDireccion);
            user.id_address = id_address;

            Console.Write("Frase de perfil: ");
            user.profile_phrase = Console.ReadLine()?.Trim() ?? string.Empty;

            _servicio.CrearUser(user);
            Console.WriteLine("✅ Usuario creado con éxito.");
        }

        private bool ContieneNumeros(string texto)
        {
            return texto.Any(char.IsDigit);
        }

        private (bool, string) EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "El email no puede estar vacío.");

            if (!email.Contains("@"))
                return (false, "El email debe contener '@'.");

            if (!email.Contains("."))
                return (false, "El email debe contener al menos un punto '.'.");

            return (true, string.Empty);
        }

    }
}
