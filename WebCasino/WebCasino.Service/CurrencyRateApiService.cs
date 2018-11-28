using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public CurrencyRateApiService(IAPIRequester apiRequester)
        {
            this.apiRequester = apiRequester;
        }

        public async Task<Dictionary<string,double>> RefreshRates()
        {
            var jsonResponse = await apiRequester.Request(connection);

            var model = JsonConvert.DeserializeObject<CurrencyRequestBindModel>(jsonResponse);

            if (model.Error == null)
            {
                return model.Rates;
            }

            throw new ApiServiceNotFoundException(model.Error);
        }
    }
}
