using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOrdersApp {
    internal class GenerateAmazonOrders {
        #region FIELDS ---------------------------------------------------------------------------------------------------
        private readonly Random _random = new(DateTime.Now.Millisecond);
        private string _filePath = "";
        private int _maxAmountSingleOrder;
        private int _amountOfOrders;
        private int _amountOfDifferentOrders;
        private readonly string[] asinCollection;
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
            get => _amountOfDifferentOrders;
            set {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
                _amountOfDifferentOrders = value;
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
            asinCollection = new string[AmountOfDifferentOrders];
            FilePath = filePath;
            StartGenerating();
        }
        #endregion

        #region METHODS ---------------------------------------------------------------------------------------------------
        private void StartGenerating() {
            // Fill the asinCollection with random asins
            for (int i = 0; i < asinCollection.Length; i++) {
                string asin = GenerateAsin();
                asinCollection[i] = asin;
            }

            // Fill the stringbuilder with a list of asins randomly chosen from de asincollection togheter with a radom amount
            StringBuilder generatedOrders = new();
            for (int i = 0; i < AmountOfOrders; i++) {
                int amount = GenerateAmount();
                int randomIndex = _random.Next(0, AmountOfDifferentOrders);
                string asin = asinCollection[randomIndex];
                generatedOrders.AppendLine($"{asin}:{amount}");
            }
            File.WriteAllText(FilePath, generatedOrders.ToString());
        }

        private int GenerateAmount() => _random.Next(1, MaxAmountSingleOrder + 1);

        private string GenerateAsin() {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 8;

            char[] asin = new char[length];

            for (int i = 0; i < length; i++) {
                asin[i] = chars[_random.Next(chars.Length)];
            }

            return "ASIN_B0" + new string(asin);
        }
        #endregion

    }
}
