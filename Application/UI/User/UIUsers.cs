using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class UIUsers
    {
        private readonly UserService _userService;
        private readonly dynamic _usuario;

        public UIUsers(UserService userService, dynamic usuario)
        {
            _userService = userService;
            _usuario = usuario;
        }

        public string InitialMenu()
        {
            return @$"
                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
                    ▒█▀▀█ ░█▀▀█ ▒█▀▄▀█ ▒█▀▀█ ▒█░▒█ ▒█▀▀▀█ ▒█░░░ ▒█▀▀▀█ ▒█░░▒█ ▒█▀▀▀ 
                    ▒█░░░ ▒█▄▄█ ▒█▒█▒█ ▒█▄▄█ ▒█░▒█ ░▀▀▀▄▄ ▒█░░░ ▒█░░▒█ ░▒█▒█░ ▒█▀▀▀ 
                    ▒█▄▄█ ▒█░▒█ ▒█░░▒█ ▒█░░░ ░▀▄▄▀ ▒█▄▄▄█ ▒█▄▄█ ▒█▄▄▄█ ░░▀▄▀░ ▒█▄▄▄
                                              
                            Bienvenido, {_usuario.first_name} {_usuario.last_name}!  
                            Créditos disponibles hoy: _usuario.credits
                            
                            🅼🅴🅽🆄
                            1. Ver perfiles y dar Like o Dislike      
                            2. Ver mis coincidencias (Matches)        
                            3. Ver estadísticas del sistema           
                            4. Ver mi perfil                          
                            0. Cerrar sesión    
                ♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥";
        }

        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine(InitialMenu());
                Console.Write("Seleccione una opción: ");

                int opcion = CampusLove.Utilidades.LeerOpcionMenuKey(InitialMenu());
                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ver perfiles y dar Like o Dislike");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ver mis coincidencias");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Ver estadísticas del sistema");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Ver mi perfil");
                        break;
                    case 0:
                        Console.Write("¿Está seguro que desea salir? (S/N): ");
                        salir = CampusLove.Utilidades.LeerTecla(); 
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
}
