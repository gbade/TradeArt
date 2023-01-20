using System;
using System.Threading.Tasks;

namespace TradeArt.Contracts
{
	public interface IHashCalculator
	{
        public Task<string> CalculateShaAsync(string path);
    }
}

