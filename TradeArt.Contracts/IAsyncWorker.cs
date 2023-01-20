using System;
using System.Threading.Tasks;

namespace TradeArt.Contracts
{
	public interface IAsyncWorker
	{
		Task<bool> FuncA();
        Task<bool> ProcessData(int data);
    }
}

