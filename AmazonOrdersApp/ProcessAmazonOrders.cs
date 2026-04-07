using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOrdersApp {
    internal class ProcessAmazonOrders {
        #region FIELDS ---------------------------------------------------------------------------------------------------
        private string _filePathInput = "";
        private string _filePathOutput = "";
        private string[] _dataArray;
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
            _dataArray = File.ReadAllLines(FilePathInput);
            StartProcessing();
        }
        #endregion

        #region METHODS ---------------------------------------------------------------------------------------------------
        private void StartProcessing() {
            PutAmountAtTheEnd(_dataArray);
            Array.Sort(_dataArray);
            ConsolidateOrders(ref _dataArray);
            File.WriteAllLines(FilePathOutput, _dataArray);
        }
        
        private static void PutAmountAtTheEnd(string[] array) {
            for (int i = 0; i < array.Length; i++) {
                string[] splitOrder = array[i].Split(';');
                Array.Reverse(splitOrder);
                array[i] = string.Join(';', splitOrder);
            }
        }

        private static void ConsolidateOrders(ref string[] array) {
            List<string> arrayToList = [.. array];
            for (int i = 0; i < arrayToList.Count - 1; i++) {
                string[] currentOrder = arrayToList[i].Split(';');
                string[] nextOrder = arrayToList[i + 1].Split(';');
                string currentAsin = currentOrder[0];
                string nextAsin = nextOrder[0];

                if (currentAsin == nextAsin) {
                    int currentAmount = int.Parse(currentOrder[1]);
                    int nextAmount = int.Parse(nextOrder[1]);
                    int totalAmount = currentAmount + nextAmount;
                    arrayToList[i] = currentAsin + ";" + totalAmount;
                    arrayToList.RemoveAt(i + 1);
                }
            }
            array = [.. arrayToList];
        }
        #endregion

    }
}
