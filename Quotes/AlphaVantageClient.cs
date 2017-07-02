using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEase;

namespace FoxMoney.Quotes {
    public interface IAlphaVantageClient {
        [Get("query?function=TIME_SERIES_DAILY_ADJUSTED&outputsize=full")]
        Task<string> GetHistoricalPrices(string symbol, string apikey, string outputsize);
    }

    public class AlphaVantageClient {
        private readonly string apikey;
        public AlphaVantageClient(string apikey) {
            this.apikey = apikey;

        }

        public async Task<IList<DataPoint>> GetHistoricalPrices(string symbol, DateTime startTime, bool full) {
            List<DataPoint> data = new List<DataPoint>();
            IAlphaVantageClient client = RestClient.For<IAlphaVantageClient>("https://www.alphavantage.co");
            string result;
            if (full) {
                result = await client.GetHistoricalPrices(symbol, this.apikey, "full");
            } else {
                result = await client.GetHistoricalPrices(symbol, this.apikey, "compact");
            }

            JObject objects = JObject.Parse(result);
            var dailyPrices = objects["Time Series (Daily)"];
            foreach(var dailyPrice in dailyPrices) {
                DataPoint insert = new DataPoint {
                Date = DateTime.Parse((dailyPrice as JProperty).Name),
                Open = dailyPrice.First["1. open"].Value<decimal>(),
                High = dailyPrice.First["2. high"].Value<decimal>(),
                Low = dailyPrice.First["3. low"].Value<decimal>(),
                Close = dailyPrice.First["4. close"].Value<decimal>(),
                AdjustedClose = dailyPrice.First["5. adjusted close"].Value<decimal>(),
                Volume = dailyPrice.First["6. volume"].Value<int>(),
                DividendAmount = dailyPrice.First["7. dividend amount"].Value<decimal>(),
                SplitCoefficient = dailyPrice.First["8. split coefficient"].Value<decimal>(),
                };

                if (insert.Date < startTime)
                    break;

                data.Add(insert);
            }

            return data;
        }

    }
}