using System;
using CampusLove.domain.Factory;
using CampusLove.Infrastructure.Pgsql;
using Npgsql;
using SistemaGestorV;

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
    ===================================================================
    ███████████████████████████████████████████████████████████████████
    ███─▄▄▄─██▀▄─██▄─▀█▀─▄█▄─▄▄─█▄─██─▄█─▄▄▄▄█▄─▄███─▄▄─█▄─█─▄█▄─▄▄─███
    ███─███▀██─▀─███─█▄█─███─▄▄▄██─██─██▄▄▄▄─██─██▀█─██─██▄▀▄███─▄█▀███
    ▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▀▀
    ===================================================================
                           1. Iniciar sesión
                           2. Registrarse
                           0. Salir
    ===================================================================";
    }

    static void Main(string[] args)
    {

     string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;Password=root123;Pooling=true;";
        IDbfactory factory = new NpgsqlgDbFactory(connStr);

        MostrarBarraDeCarga();

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine(MainMenu());
            Console.Write("Seleccione una opción: ");
            int opcion = SistemaGestorV.Utilidades.LeerOpcionMenuKey(MainMenu());
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("========= MENÚ DE INICIO DE SESIÓN =========");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("========= MENÚ DE REGISTRO =========");
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