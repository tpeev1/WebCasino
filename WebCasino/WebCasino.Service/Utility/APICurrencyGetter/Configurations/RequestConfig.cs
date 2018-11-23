using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Utility.APICurrencyGetter.Configurations
{
	public static class RequestConfig
	{
		/// <summary>
		/// MAIN REQUEST URI
		/// </summary>
		public const string REQUEST_URI = "https://api.exchangeratesapi.io/";

		/// <summary>
		/// Add All query parameters for URI
		/// </summary>
		public static class QueryParameters
		{
			public const string LATEST_CURRENCY = "latest";
		}
	}
}
