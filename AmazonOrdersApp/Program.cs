using System.Runtime.CompilerServices;

namespace AmazonOrdersApp {
    internal class Program {
        // 500 000 producten in een tekst bestand, 400 000 verschillende soorten
        // Verschillende (soms meerdere keren voorkomende) soorten productcodes en een aantal
        // Geeft de 15 meest verkochte producten en het aantal weer
        // Binnen de 4 seconden!
        private const string FilePath = "amazon_orders.txt";
        static void Main() {

            while (true) {
                Console.Write("[G]enereer een nieuwe dataset of [V]erwerk de bestaande dataset? : ");
                string input = Console.ReadLine()!.Trim().ToLower();

                if (input == "g") {
                    GenerateData();
                    Console.WriteLine("Nieuwe dataset gegenereerd!\n");

                } else if (input == "v") {
                    ProcessAmazonOrders orderList = new(FilePath);
                    orderList.PrintAmount(50);
                }
                else {
                    continue;
                }
            }

            Console.WriteLine("\nEND OF PROGRAM");
            Console.ReadKey();
        }

        private static void GenerateData() {
            int amountOfOrders = 500000;
            int amountOfDifferentOrders = 400000;
            int maxAmountSingleOrder = 20;
            _ = new GenerateAmazonOrders(amountOfOrders, amountOfDifferentOrders, maxAmountSingleOrder, FilePath);
        }

    }
}
