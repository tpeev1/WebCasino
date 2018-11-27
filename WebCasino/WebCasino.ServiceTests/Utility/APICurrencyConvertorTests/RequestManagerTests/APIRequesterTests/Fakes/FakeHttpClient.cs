using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.APIRequesterTests.Fakes
{
	public class FakeHttpClient : HttpClient
	{
		public async Task<string> GetStringAsync(string connection)
		{
			return "Works";
		}
	}
}
