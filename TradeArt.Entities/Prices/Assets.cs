using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TradeArt.Entities
{
	public class Assets
	{
        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("assetSymbol")]
        public string AssetSymbol { get; set; }

        [JsonProperty("marketCap")]
        public object MarketCap { get; set; }

        [JsonProperty("markets")]
        public List<Market> Markets { get; set; }
    }
}

