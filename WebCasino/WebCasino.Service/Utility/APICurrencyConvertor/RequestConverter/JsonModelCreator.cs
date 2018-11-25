using Newtonsoft.Json;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter
{
	public class JsonModelCreator
	{
		public CurrencyRequestBindModel JsonToModelDeserializer(string inputJson)
		{
			var apiRequestResult = JsonConvert.DeserializeObject<CurrencyRequestBindModel>(inputJson);
			//Validations

			return apiRequestResult;
		}
	}
}