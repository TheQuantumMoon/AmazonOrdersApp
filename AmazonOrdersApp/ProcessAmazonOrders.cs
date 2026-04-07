using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOrdersApp {
    internal class ProcessAmazonOrders {

        private string _filePath = "";
        private readonly string[] dataArray;

        private string FilePath {
            get => _filePath;
            init => _filePath = value;
        }

        public ProcessAmazonOrders(string filePath) {
            FilePath = filePath;
            dataArray = ConsolodateData(File.ReadAllLines(FilePath));
        }

        private string[] ConsolodateData(string[] dataArray) {
            List<string> consolodatedData = [];
            for (int i = 0; i < dataArray.Length; i++) {
                string currentAsin = dataArray[i];
                int matchingIndex = consolodatedData.IndexOf(currentAsin);
                if (matchingIndex < 0) {
                    consolodatedData.Add(currentAsin);
                } else {
                    string matchingAsin = consolodatedData[matchingIndex];
                    int newAmount = int.Parse(currentAsin.Split(':')[1]);
                    int oldAmount = int.Parse(matchingAsin.Split(':')[1]);
                    consolodatedData[matchingIndex] = matchingAsin.Split(':')[0] + ":" + (newAmount + oldAmount).ToString();
                }
            }

            return [.. consolodatedData];
        }

        public void PrintAmount(int amount) {
            for (int i = 0; i < amount; i++) {
                Console.WriteLine(dataArray[i]);
            }
        }

        public string[] GetTop(int top) {


            return default;
        }

    }
}
