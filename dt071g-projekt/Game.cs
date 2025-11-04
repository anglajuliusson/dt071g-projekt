using System;
using System.IO;

namespace rockPaperScissorsGame {

public class Game {
        // Statistik på resultat

        // Spelarens resultat
        private int wins = 0; // Vinster
        private int losses = 0; // Förluster
        private int draws = 0; // Oavgjorda

        // Skapar en instans av C#-klassen Random för att  slumpa datorns val
        private Random random = new Random();

        // Namnet på filen som används för att spara spelarens resultat
        private string resultsFile = "results.txt";

        // Ladda tidigare resultat
        public void LoadResults()
        {
            try
            {
                // Kontrollera om filen finns
                if (File.Exists(resultsFile))
                {
                    // Läs hela filens innehåll och dela upp med kommatecken
                    string[] parts = File.ReadAllText(resultsFile).Split(',');

                    // Kontrollera att filen innehåller exakt tre värden
                    if (parts.Length == 3)
                    {
                        // Omvandla textvärden till heltal
                        int.TryParse(parts[0], out wins);
                        int.TryParse(parts[1], out losses);
                        int.TryParse(parts[2], out draws);
                    }
                }
            }
            catch (Exception ex)
            {
                // Felmeddelande
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Kunde inte läsa resultat: {ex.Message}");
            }
        }

        // Spara resultat
        private void SaveResults()
        {
            try
            {
                // Skriver över eller skapar filen med de aktuella värderna
                File.WriteAllText(resultsFile, $"{wins},{losses},{draws}");
            }
            catch (Exception ex)
            {
                // Felmeddelande
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Kunde inte spara statistik: {ex.Message}");
            }
        }

        // Visa resultat

        // Skriver ut tidiagre resultat till konsolen
        public void ShowResults()
        {
            Console.WriteLine(); // Tom rad
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--- Tidigare resultat ---");
            Console.WriteLine($"Vinster: {wins}");
            Console.WriteLine($"Förluster: {losses}");
            Console.WriteLine($"Oavgjort: {draws}");
        }

        // Spela en match (bäst av tre)
        public void PlayRound()
        {
            // Räknare för poäng i matchen
            int playerScore = 0; // Spelarens vinster i denna match
            int computerScore = 0; // Datorns vinster i denna match

            Console.WriteLine(); // Tom rad
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BÄST AV TRE ---");

            // Loopa tills någon av spelarna får två vinster (bäst av tre)
            while (playerScore < 2 && computerScore < 2)
            {
                Console.WriteLine(); // Tom rad
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Ny runda!");
                Console.WriteLine(); // Tom rad
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Välj: 1 = Sten, 2 = Sax, 3 = Påse");
                string input = Console.ReadLine(); // Användarens val

                // Omvandla string så att det går att jämföra med nummer
                // Kontrollera giltigt värde
                // Villkor: input kan inte vara tom, kan konverteras till int, mellan 1 och 3
                if ( string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int playerChoice) || playerChoice < 1 || playerChoice > 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ogiltigt val! Försök igen.\n");
                    continue;
                }

                // Slumpa fram datorns val
                int computerChoice = random.Next(1, 4); // Random.Next(1,4) ger 1, 2 eller 3

                // Visa vad både spelare och dator valde
                Console.WriteLine(); // Tom rad
                Console.WriteLine($"Du valde {ChoiceToString(playerChoice)}");
                Console.WriteLine($"Datorn valde {ChoiceToString(computerChoice)}");

                // Avgör vem som vann rundan
                int result = DetermineWinner(playerChoice, computerChoice);

                // Hantera resultat av rundan
                if (result == 1)
                {
                    Console.WriteLine(); // Tom rad
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Du vann rundan!");
                    playerScore++; // Öka spelarens poäng
                }
                else if (result == -1) // -1 = datorn vann
                {
                    Console.WriteLine(); // Tom rad
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du förlorade rundan!");
                    computerScore++; // Öka datorns poäng
                }
                else // 0 = Oavgjort
                {
                    Console.WriteLine(); // Tom rad
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Oavgjort!");
                }

                // Visa aktuell ställning efter rundan
                Console.WriteLine(); // Tom rad
                Console.WriteLine($"Ställning: Du {playerScore} - {computerScore} Datorn");
            }

            // När någon nått 2 poäng är matchen över
            if (playerScore > computerScore) // Spelarens poäng är större än datorns
            {
                Console.WriteLine(); // Tom rad
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Du vann matchen!");
                wins++; // Uppdatera den totala statistiken för vinster
            }
            else if (playerScore < computerScore) // Spelarens poäng är mindre än datorns
            {
                Console.WriteLine(); // Tom rad
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Datorn vann matchen!");
                losses++; // Uppdatera den totala statistiken för förlust
            }
            else 
            {
                Console.WriteLine(); // Tom rad
                Console.ForegroundColor = ConsoleColor.Yellow;
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