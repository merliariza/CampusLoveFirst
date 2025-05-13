using System;
using Npgsql;

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
    static void Main(string[] args)
    {
        string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;Password=root123;Pooling=true;";
        using var conn = new NpgsqlConnection(connStr);
        MostrarBarraDeCarga();
        try
        {
            conn.Open();
            Console.WriteLine("✅ ¡Conexión exitosa a la base de datos!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error al conectar a la base de datos:");
            Console.WriteLine(ex.Message);
        }
    }
}