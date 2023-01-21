using System;
namespace TradeArt.Entities.DataProcessor
{
	public class AsyncResponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}
}

