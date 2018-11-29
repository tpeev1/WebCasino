using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.APICurrencyConvertor.Exceptions;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.Service
{
    public class CurrencyRateApiService : ICurrencyRateApiService
    {
        private readonly IAPIRequester apiRequester;

        private const string connection = "https://api.exchangeratesapi.io/latest?base=USD&symbols=EUR,BGN,GBP";

        private ConcurrentDictionary<string, double> rates;

        private DateTime lastUpdate;


        public CurrencyRateApiService(IAPIRequester apiRequester)
        {
            this.apiRequester = apiRequester;
        }

        public async Task<ConcurrentDictionary<string,double>> GetRatesAsync()
        {
            if(lastUpdate.AddHours(24) <= DateTime.UtcNow || rates == null)
            {
                var dic = this.RefreshRates();
                rates = new ConcurrentDictionary<string,double>(await this.RefreshRates());
            }
            return new ConcurrentDictionary<string,double>(rates);
        }

        private async Task<Dictionary<string,double>> RefreshRates()
        {
            var jsonResponse = await apiRequester.Request(connection);

            var model = JsonConvert.DeserializeObject<CurrencyRequestBindModel>(jsonResponse);

            if (model.Error == null)
            {
                lastUpdate = DateTime.UtcNow;
                return model.Rates; 
            }

            throw new ApiServiceNotFoundException(model.Error);
        }
    }
}
