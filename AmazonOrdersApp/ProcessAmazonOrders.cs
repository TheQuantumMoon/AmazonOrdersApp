using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOrdersApp {
    internal class ProcessAmazonOrders {
        #region FIELDS ---------------------------------------------------------------------------------------------------
        private string _filePathInput = "";
        private string _filePathOutput = "";
        private SortedDictionary<string, int> _orders = [];
        #endregion

        #region PROPERTIES ---------------------------------------------------------------------------------------------------
        private string FilePathInput {
            get => _filePathInput;
            init => _filePathInput = value;
        }
        private string FilePathOutput {
            get => _filePathOutput;
            set => _filePathOutput = value;
        }
        #endregion

        #region CONSTRUCTORS ---------------------------------------------------------------------------------------------------
        public ProcessAmazonOrders(string filePathInput, string filePathOutput) {
            FilePathInput = filePathInput;
            FilePathOutput = filePathOutput;
            foreach (string line in File.ReadLines(FilePathInput)) {
                string[] order = line.Split(';');
                int amount = int.Parse(order[0]);
                string asin = order[1];

                if (_orders.TryGetValue(asin, out int existingValue)) {
                    _orders[asin] = existingValue + amount;
                }
                else {
                    _orders.Add(asin, amount);
                }
            }
            StartProcessing();
        }
        #endregion

        #region METHODS ---------------------------------------------------------------------------------------------------
        private void StartProcessing() {
            StoreData();
        }

        private void StoreData() {
            var sortedAndConsolidatedOrders = _orders.Select(order => order.Key + ";" + order.Value);
            File.WriteAllLines(_filePathOutput, sortedAndConsolidatedOrders);
        }
        #endregion

    }
}
