using System;
using GraphQL;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Client.Abstractions;
using Moq;
using TradeArt.Contracts.GraphQL;
using Xunit;
using TradeArt.Business.GraphQL;
using TradeArt.Entities;
using TradeArt.Entities.Prices.Response;

namespace TradeArt.UnitTests.Services
{
	public class GraphQLServiceTest
	{
        private IGraphQLService _graphQLService;
        private Mock<IGraphQLClientFactory> _graphQlClientFactory;
        private Mock<IGraphQLClient> _graphQlClient;

        public GraphQLServiceTest()
		{
            _graphQlClient = new Mock<IGraphQLClient>();
            _graphQlClientFactory = new Mock<IGraphQLClientFactory>();
            _graphQlClientFactory.Setup(g => g.GetGraphQLClient()).Returns(_graphQlClient.Object);

            _graphQLService = new GraphQLService(_graphQlClientFactory.Object);
        }

        [Fact]
        public async Task Should_Call_Client_Five_Times_And_Return_One_Hundred_Items()
        {
            // Arrange
            var batchSize = 20;
            _graphQlClient.Setup(c => c.SendQueryAsync<AssetsWithPricesQuery>(It.IsAny<GraphQLRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GraphQLResponse<AssetsWithPricesQuery>()
                {
                    Data = new AssetsWithPricesQuery()
                    {
                        Assets = Enumerable.Repeat(new Assets(), batchSize).ToList()
                    }
                });

            // Act
            var res = await _graphQLService.FetchAssetsWithPrices();

            // Assert
            _graphQlClient.Verify(c => c.SendQueryAsync<AssetsWithPricesQuery>(It.IsAny<GraphQLRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(5));
            Assert.Equal(100, res.Count());
        }
    }
}

