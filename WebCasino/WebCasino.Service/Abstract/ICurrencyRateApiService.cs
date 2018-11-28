using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCasino.Service.Abstract
{
    public interface ICurrencyRateApiService
    {
        Task<Dictionary<string, double>> RefreshRates();
    }
}