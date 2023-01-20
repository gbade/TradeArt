using System;
using Moq;
using TradeArt.Contracts;
using TradeArt.Business;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace TradeArt.UnitTests.Services
{
	public class AsyncWorkerTest
	{
		private IAsyncWorker _worker;
		private Mock<IAsyncWorker> _service;

        public AsyncWorkerTest()
        {
            _service = new Mock<IAsyncWorker>();
            _worker = new AsyncWorker();
        }

        [Fact]
        public void Should_Return_Successful()
        {
            //Arrange
            _service.Setup(s => s.ProcessData(It.IsAny<int>())).ReturnsAsync(true);

            //Act
            var isSuccesful = _worker.FuncA().Result;

            //Assert
            Assert.False(isSuccesful == false);
        }
    }
}

