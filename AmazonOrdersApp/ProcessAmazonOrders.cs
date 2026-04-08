namespace AmazonOrdersApp {
    internal class ProcessAmazonOrders {
        #region FIELDS ---------------------------------------------------------------------------------------------------
        private string _filePathInput = "";
        private string _filePathOutput = "";
        private readonly Dictionary<string, int> _orders = [];
        private Dictionary<string, int> _ordersSortedByKey = [];
        private Dictionary<string, int> _ordersSortedByValue = [];
        #endregion

        #region PROPERTIES ---------------------------------------------------------------------------------------------------
        private string FilePathInput {
            get => _filePathInput;
            init {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                _filePathInput = value;
            }
        }
        private string FilePathOutput {
            get => _filePathOutput;
            init {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                _filePathOutput = value;
            }
        }
        #endregion

        #region CONSTRUCTORS ---------------------------------------------------------------------------------------------------
        // Zorgt dat bij het aanmaken van het object, _orders wordt gevuld met unieke keys, duplicate keys worden niet toegevoegd, maar hun value's worden wel opgeteld
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
        // Spreekt voor zichzelf
        private void StartProcessing() {
            _ordersSortedByKey = GetOrdersSortedByKey();
            _ordersSortedByValue = GetSortedOrdersByValue();
            StoreOrders(_ordersSortedByKey);
        }

        // Geeft een Dictionary terug, dat de _orders zijn, gesorteerd op de key (oplopend)
        private Dictionary<string, int> GetOrdersSortedByKey() {
            return _orders.OrderBy(item => item.Key).ToDictionary();
        }

        // Geeft een Dictionary terug, dat de _orders zijn, gesorteerd op de value (aflopend)
        private Dictionary<string, int> GetSortedOrdersByValue() {
            return _orders.OrderByDescending(item => item.Value).ToDictionary();
        }

        // Schrijft een gegeven Dictionary weg naar een tekstbestand op locatie _filePathOutput
        private void StoreOrders(Dictionary<string, int> orders) {
            var sortedAndConsolidatedOrders = orders.Select(order => order.Key + ";" + order.Value);
            File.WriteAllLines(_filePathOutput, sortedAndConsolidatedOrders);
        }

        // Geeft een string array terug, gevuld met de x hoogste orders, gebaseerd op hun verkoopaantal
        public string[] GetTopXamount(int amount) {
            string[] output = new string[amount];
            int index = 0;
            foreach (var item in _ordersSortedByValue) {
                if (index >= amount) break;
                output[index] = item.Key + ": " + item.Value;
                index++;
            }
            return output;
        }

        // Geeft het verkoopaantal terug van een asin
        public int? GetAmountByAsin(string asin) {
            if (_orders.ContainsKey(asin)) {
                return _orders[asin];
            } else {
                return null;
            }
            
        }
        #endregion

    }
}
