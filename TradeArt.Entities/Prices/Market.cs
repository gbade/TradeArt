using System;
using Newtonsoft.Json;

namespace TradeArt.Entities
{
	public class Market
	{
        [JsonProperty("marketSymbol")]
        public string MarketSymbol { get; set; }

        [JsonProperty("ticker")]
        public Ticker Ticker { get; set; }
    }
}

