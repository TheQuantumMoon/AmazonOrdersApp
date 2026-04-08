using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AmazonOrdersApp {
    internal class Program {
        // 500 000 producten in een tekst bestand
        // Geeft de 15 meest verkochte producten en het aantal weer
        // Binnen de 4 seconden!
        private const string _filePathInput = "amazon_orders.txt";
        private const string _filePathOutput = "amazon_orders_verwerkt.txt";
        static void Main() {

            while (true) {
                // Vraag de gebruiker om een actie
                Console.Write("[G]enereer een nieuwe dataset of [V]erwerk de bestaande dataset? : ");
                string input = Console.ReadLine()!.Trim().ToLower();

                if (input == "g") {
                    // Genereer een tekstbestand met amazon orders
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    GenerateOrders(_filePathInput);
                    stopwatch.Stop();
                    Console.WriteLine($"Nieuwe dataset gegenereerd in {stopwatch.ElapsedMilliseconds} ms!\n");

                } else if (input == "v") {
                    // Verwerk een tekstbestand met amazon orders en steek dit in een object
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    ProcessAmazonOrders orderList = new(_filePathInput, _filePathOutput);
                    stopwatch.Stop();
                    Console.WriteLine($"\nUitvoeringstijd: {stopwatch.ElapsedMilliseconds} ms\n");

                    // Toon de top x aantal mamazon orders gebaseerd op het verkoopsaantal
                    int amount = 15;
                    string[] topXamount = orderList.GetTopXamount(amount);
                    Console.WriteLine($"Top {amount} meest verkochte producten:");
                    foreach (var item in topXamount) {
                        Console.WriteLine(item);
                    }

                    while (true) {
                        // Vraag de gebruiker om een ASIN en geef het corresponderende verkoopsaantal terug
                        Console.Write("\nZoek de totale verkoopshoeveelheid op van product met ASIN code ?: ");
                        input = Console.ReadLine()!.Trim();

                        int? soldAmount = orderList.GetAmountByAsin(input);
                        if (soldAmount.HasValue) {
                            Console.WriteLine("Totale verkoopshoeveelheid : " + soldAmount);
                        } else {
                            Console.WriteLine("ASIN code niet teruggevonden.");
                        }
                    }
                }
            }
        }

        // Genereert een tekstbestand met amazon orders aan de hand van 4 parameters
        private static void GenerateOrders(string filePath) {
            int amountOfOrders = 500000;
            int amountOfPossibleDifferentOrders = 500000;
            int maxAmountSingleOrder = 200;
            _ = new GenerateAmazonOrders(amountOfOrders, amountOfPossibleDifferentOrders, maxAmountSingleOrder, filePath);
        }
    }
}
