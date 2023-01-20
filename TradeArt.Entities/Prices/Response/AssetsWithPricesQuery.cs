using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TradeArt.Entities.Prices.Response
{
	public class AssetsWithPricesQuery
	{
        [JsonProperty("assets")]
        public List<Assets> Assets { get; set; }
    }
}

