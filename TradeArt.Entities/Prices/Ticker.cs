using System;
using Newtonsoft.Json;

namespace TradeArt.Entities
{
	public class Ticker
	{
        [JsonProperty("lastPrice")]
        public string LastPrice { get; set; }
    }
}

