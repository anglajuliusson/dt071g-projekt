// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace rockPaperScissorsGame {

    class Program {
        public static void Main(string[] args)
    {
        Game game = new Game(); // Skapa spel-objekt
        game.LoadResult(); // Läs in tidigare resultat vid start
        bool running = true;

        while (running) 
        {
        Console.WriteLine("=== STEN SAX PÅSE ===");
        Console.WriteLine("Välj ett alternativ: ");
        Console.WriteLine("1. Starta spel");
        Console.WriteLine("2. Visa tidigare resultat");
        Console.WriteLine("3. Avsluta");
        
        var choice = Console.ReadLine(); // Val av alternativ

        // Alternativ 1 - Starta spel
        if (choice == "1") {
            game.PlayRound();
        }

        // Alternativ 2 - Visa tidigare resultat
        if (choice == "2") {
            game.ShowResult();
        }

        // Alternativ 4 - Avsluta program
        if (choice == "3") {
            break;
        }

        // Ogiltigt alternativ
        if (choice == default)
            Console.WriteLine("Ogiltigt val, försök igen!");
            break;
        }
    }
    }

    public class Game {

    }
}