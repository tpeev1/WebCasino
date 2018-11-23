using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Utility.APICurrencyGetter.Models
{
	public class CurrencyBindModel
	{
		[JsonProperty("base")]
		public string Base { get; set; }
		[JsonProperty("date")]
		public string Date { get; set; }
		[JsonProperty("rates")]
		public Dictionary<string, decimal> Rates { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }
	}
}
