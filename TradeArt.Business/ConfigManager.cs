using System;
using TradeArt.Contracts;

namespace TradeArt.Business
{
	public class ConfigManager : IConfigManager
	{
		public string BaseUrl { get; set; }
	}
}

