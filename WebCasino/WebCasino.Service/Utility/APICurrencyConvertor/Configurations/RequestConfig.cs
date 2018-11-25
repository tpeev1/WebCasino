namespace WebCasino.Service.Utility.APICurrencyConvertor.Configurations
{
	public static class RequestConfig
	{
		public const string API_URI = "https://api.exchangeratesapi.io/";

		public const int MaxRetryAttempts = 3;
		public const string BaseCurrency = "USD";

		public static class QueryParameters
		{
			//public const string API_QUERY_LATEST = "latest";

			//Concatenate base after =
			public const string API_QUERY_LATEST_BASE = "latest?base=";

			//public const string API_QUERY_LATEST_BASE_EUR = "latest?symbols=USD,GBP,BGN";

			//Concatenate base after =
			//{0} = start date = 2018-01-01, {1} end date in same format
			//public const string API_QUERY_HISTORICAL_FROM_TO_BASE = "history?start_at={0}&end_at={1}&base=";
			//public const string API_QUERY_HISTORICAL_FROM_TO_BASE_EUR = "history?start_at={0}&end_at={1}&symbols=USD,GBP,BGN";
		}
	}
}