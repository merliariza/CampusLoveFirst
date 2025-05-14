using CampusLove;
using CampusLove.Application.UI.User;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces; 

internal class Program
{   private static void MostrarBarraDeCarga()
    {
        Console.Write("Cargando: ");
        for (int i = 0; i <= 20; i++)
        {
            Console.Write("■");
            Thread.Sleep(100);
        }
        Console.WriteLine("\n");
    }
        private static string MainMenu()
    {
        return @"
    ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
    ███████████████████████████████████████████████████████████████████
    ███─▄▄▄─██▀▄─██▄─▀█▀─▄█▄─▄▄─█▄─██─▄█─▄▄▄▄█▄─▄███─▄▄─█▄─█─▄█▄─▄▄─███
    ███─███▀██─▀─███─█▄█─███─▄▄▄██─██─██▄▄▄▄─██─██▀█─██─██▄▀▄███─▄█▀███
    ▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▀▀
    ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
                           1. Iniciar sesión
                           2. Registrarse
                           0. Salir
    ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥";
    }

    static void Main(string[] args)
{
    string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;password=root123;Pooling=true;";
    IDbfactory factory = new NpgsqlDbFactory(connStr);

    var userService = new UserService(factory.CreateUsersRepository());
    var genderService = new GendersService(factory.CreateGendersRepository());
    var careerService = new CareersService(factory.CreateCareersRepository());
    var addressService = new AddressesService(factory.CreateAddressesRepository(), connStr);
    var interestsService = new InterestsService(factory.CreateInterestsRepository(), connStr);

    MostrarBarraDeCarga();

    bool salir = false;
    while (!salir)
    {
        Console.Clear();
        Console.WriteLine(MainMenu());
        Console.Write("Seleccione una opción: ");
        int opcion = CampusLove.Utilidades.LeerOpcionMenuKey(MainMenu());
        Console.WriteLine();

        switch (opcion)
        {
            case 1:
                Console.Clear();
                var login = new LoginUser(userService);
                login.Ejecutar();
                break;
            case 2:
                Console.Clear();
                var registrar = new CreateUser(userService, genderService, careerService, addressService, interestsService);
                registrar.Ejecutar();
                break;
            case 0:
                Console.WriteLine("¿Está seguro que desea salir? (S/N): ");
                salir = Utilidades.LeerTecla();
                break;
            default:
                Console.WriteLine("Ingrese una opción válida.");
                break;
        }

        if (!salir)
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    Console.WriteLine("Presione cualquier tecla para salir...");
    Console.ReadKey();
}
}