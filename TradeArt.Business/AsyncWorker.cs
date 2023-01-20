using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeArt.Contracts;

namespace TradeArt.Business
{
	public class AsyncWorker : IAsyncWorker
    {
        private HashSet<Task<bool>> tasks;
		public AsyncWorker()
		{
            tasks = new HashSet<Task<bool>>();
		}

        public async Task<bool> FuncA()
        {
            for (int i = 1; i < 1000; i++)
                tasks.Add(ProcessData(i));

            var results = await Task.WhenAll(tasks);

            return WasResultSuccessful(results);
        }

        //FuncB
        public async Task<bool> ProcessData(int data)
        {
            try
            {
                await Task.Delay(100);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool WasResultSuccessful(bool[] results)
        {
            var result = new List<bool>(results);
            return !result.Any(r => false);
        }
    }
}

