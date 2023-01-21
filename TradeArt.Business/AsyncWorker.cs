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
        private readonly IProcessdata _dataProcessor;

		public AsyncWorker(IProcessdata dataProcessor)
		{
            tasks = new HashSet<Task<bool>>();
            _dataProcessor = dataProcessor;
		}

        public async Task<bool> FuncA()
        {
            for (int i = 1; i < 1000; i++)
                tasks.Add(_dataProcessor.ProcessAsync(i));

            var results = await Task.WhenAll(tasks);

            return WasResultSuccessful(results);
        }

        private bool WasResultSuccessful(bool[] results)
        {
            var result = new List<bool>(results);
            return !result.Any(r => false);
        }
    }

    public class ProcessData : IProcessdata
    {
        //FuncB
        public async Task<bool> ProcessAsync(int data)
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
    }
}

