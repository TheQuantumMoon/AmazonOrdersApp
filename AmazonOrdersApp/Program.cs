using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AmazonOrdersApp {
    internal class Program {
        // 500 000 producten in een tekst bestand, 400 000 verschillende soorten
        // Verschillende (soms meerdere keren voorkomende) soorten productcodes en een aantal
        // Geeft de 15 meest verkochte producten en het aantal weer
        // Binnen de 4 seconden!
        private const string _filePathInput = "amazon_orders.txt";
        private const string _filePathOutput = "amazon_orders_verwerkt.txt";
        static void Main() {

            while (true) {
                Console.Write("[G]enereer een nieuwe dataset of [V]erwerk de bestaande dataset? : ");
                string input = Console.ReadLine()!.Trim().ToLower();

                if (input == "g") {
                    GenerateData(_filePathInput);
                    Console.WriteLine("Nieuwe dataset gegenereerd!\n");

                } else if (input == "v") {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    ProcessAmazonOrders orderList = new(_filePathInput, _filePathOutput);
                    stopwatch.Stop();

                    long ms = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine($"\nUitvoeringstijd: {ms} ms\n");
                }
                else {
                    continue;
                }
            }
        }

        private static void GenerateData(string filePath) {
            int amountOfOrders = 500000;
            int amountOfDifferentOrders = 500000;
            int maxAmountSingleOrder = 200;
            _ = new GenerateAmazonOrders(amountOfOrders, amountOfDifferentOrders, maxAmountSingleOrder, filePath);
        }

    }
}
