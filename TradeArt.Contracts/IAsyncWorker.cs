using System;
using System.Threading.Tasks;

namespace TradeArt.Contracts
{
	public interface IAsyncWorker
	{
		Task<bool> FuncA();
    }

	public interface IProcessdata
	{
		Task<bool> ProcessAsync(int data);
	}
}

