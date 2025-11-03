// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace rockPaperScissorsGame {

    class Program {
        public static void Main(string[] args)
    {
        Game game = new Game(); // Skapa spel-objekt
        game.LoadResults(); // Läs in tidigare resultat vid start
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
            game.ShowResults();
        }

        // Alternativ 4 - Avsluta program
        if (choice == "3") {
            break;
        }

        // Ogiltigt alternativ
        if (choice != "1" && choice != "2" && choice != "3") {
            Console.WriteLine("Ogiltigt val, försök igen!");
            break;
        }
    }
    }
    }

    public class Game {

        // Spela en match (bäst av tre)
        public void PlayRound()
        {
            // Räknare för poäng i matchen
            int playerScore = 0; // Spelarens vinster i denna match
            int computerScore = 0; // Datorns vinster i denna match

            Console.WriteLine("--- BÄST AV TRE ---");

            // Loopa tills någon av spelarna får två vinster (bäst av tre)
            while (playerScore < 2 && computerScore < 2)
            {
                Console.WriteLine("Välj: 1 = Sten, 2 = Sax, 3 = Påse");
                string input = Console.ReadLine(); // Användarens val

                // Omvandla string så att det går att jämföra med nummer
                // Kontrollera giltigt värde
                // Villkor: input kan inte vara tom, kan konverteras till int, mellan 1 och 3
                if ( string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int playerChoice) || playerChoice < 1 || playerChoice > 3)
                {
                    Console.WriteLine("Ogiltigt val! Försök igen.\n");
                    continue;
                }

                // Slumpa fram datorns val
                int computerChoice = random.Next(1, 4); // Random.Next(1,4) ger 1, 2 eller 3

                // Visa vad både spelare och dator valde
                Console.WriteLine($"Du valde {ChoiceToString(playerChoice)}");
                Console.WriteLine($"Datorn valde {ChoiceToString(computerChoice)}");

                // Avgör vem som vann rundan
                int result = DetermineWinner(playerChoice, computerChoice);

                // Hantera resultat av rundan
                if (result == 1)
                {
                    Console.WriteLine("Du vann rundan!");
                    playerScore++; // Öka spelarens poäng
                }
                else if (result == -1) // -1 = datorn vann
                {
                    Console.WriteLine("Du förlorade rundan!");
                    computerScore++; // Öka datorns poäng
                }
                else // 0 = Oavgjort
                {
                    Console.WriteLine("Oavgjort!");
                }

                // Visa aktuell ställning efter rundan
                Console.WriteLine($"Ställning: Du {playerScore} - {computerScore} Datorn");
            }

            // När någon nått 2 poäng är matchen över
            if (playerScore > computerScore)
            {
                Console.WriteLine("Du vann matchen!");
                wins++; // Uppdatera den totala statistiken för vinster
            }
            else
            {
                Console.WriteLine("Datorn vann matchen!");
                losses++; // Uppdatera den totala statistiken för förlust
            }
            else 
            {
                Console.WriteLine("Det blev oavgjort!");
                draws++; // Uppdatera den totala statistiken för oavgjorda matcher
            }

            SaveResults(); // Uppdatera filen efter varje match
        }

        // Avgör vinnare för en enskildrunda
        private int DetermineWinner(int player, int computer)
        {
            if (player == computer) // Samma val = oavgjort
            {
                draws++; // Uppdatera statistik
                return 0; // Returnera 0 = oavgjort
            }
            // Spelarens vinstkombinationer
            else if ((player == 1 && computer == 2) || // Sten slår Sax
                     (player == 2 && computer == 3) || // Sax slår Påse
                     (player == 3 && computer == 1)) // Påse slår Sten
            {
                return 1; // spelaren vann
            }
            else
            {
                return -1; // datorn vann
            }
        }

        // Gör om siffror till text för att skriva ut valen
        private string ChoiceToString(int choice)
        {
            return choice switch
            {
                1 => "Sten",
                2 => "Sax",
                3 => "Påse",
                _ => "Okänt val" // Säkerhetsfall, bör aldrig inträffa
            };
        }
    }
}