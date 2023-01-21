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
        private Mock<IProcessdata> _funcB;

        public AsyncWorkerTest()
        {
            _funcB = new Mock<IProcessdata>();
            _worker = new AsyncWorker(_funcB.Object);
        }

        [Fact]
        public void Should_Return_Successful()
        {
            //Arrange
            _funcB.Setup(s => s.ProcessAsync(It.IsAny<int>())).ReturnsAsync(true);

            //Act
            var isSuccesful = _worker.FuncA().Result;

            //Assert
            Assert.False(isSuccesful == false);
        }
    }
}

