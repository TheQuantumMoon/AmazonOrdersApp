using System.Text;

namespace AmazonOrdersApp {
    internal class GenerateAmazonOrders {
        #region FIELDS ---------------------------------------------------------------------------------------------------
        private readonly Random _random = new(DateTime.Now.Millisecond);
        private string _filePath = "";
        private int _maxAmountSingleOrder;
        private int _amountOfOrders;
        private int _amountOfPossibleDifferentOrders;
        #endregion

        #region PROPERTIES ---------------------------------------------------------------------------------------------------
        private int AmountOfOrders {
            get => _amountOfOrders;
            set {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
                _amountOfOrders = value;
            }
        }
        private int AmountOfDifferentOrders {
            get => _amountOfPossibleDifferentOrders;
            set {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
                _amountOfPossibleDifferentOrders = value;
            }
        }
        private int MaxAmountSingleOrder {
            get => _maxAmountSingleOrder;
            set {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
                _maxAmountSingleOrder = value;
            }
        }
        private string FilePath {
            get => _filePath;
            init => _filePath = value;
        }
        #endregion

        #region CONSTRUCTORS ---------------------------------------------------------------------------------------------------
        public GenerateAmazonOrders(int amountOfOrders, int amountOfDifferentOrders, int maxAmountSingleOrder, string filePath) {
            AmountOfOrders = amountOfOrders;
            AmountOfDifferentOrders = amountOfDifferentOrders;
            MaxAmountSingleOrder = maxAmountSingleOrder;
            FilePath = filePath;
            StartGenerating();
        }
        #endregion

        #region METHODS ---------------------------------------------------------------------------------------------------
        private void StartGenerating() {
            // Vul de asinCollection met willekeurige asin's
            HashSet<string> asinCollectionHash = [];
            for (int i = 0; i < AmountOfDifferentOrders; i++) {
                string asin;
                do {
                    asin = GenerateAsin();
                }
                while (asinCollectionHash.Contains(asin));
                asinCollectionHash.Add(asin);
            }
            string[] asinCollection = [.. asinCollectionHash];

            // Vul de StringBuilder met een lijst van asin's, willekeurig gekozen van asincommection + een willikeirig aantal
            StringBuilder generatedOrders = new();
            for (int i = 0; i < AmountOfOrders; i++) {
                int amount = GenerateAmount();
                int randomIndex = _random.Next(0, AmountOfDifferentOrders);
                string asin = asinCollection[randomIndex];

                generatedOrders.AppendLine($"{amount};{asin}");
            }
            File.WriteAllText(FilePath, generatedOrders.ToString());
        }

        // Genereert een int van 1 tot en met MaxAmountSingleOrder
        private int GenerateAmount() => _random.Next(1, MaxAmountSingleOrder + 1);

        // Genereert een willekeurige asin code
        private string GenerateAsin() {
            int length = 8;
            int[] asin = new int[length];

            for (int i = 0; i < length; i++) {
                asin[i] = _random.Next(10);
            }

            return "ASIN_B0" + string.Concat(asin);
        }
        #endregion

    }
}
