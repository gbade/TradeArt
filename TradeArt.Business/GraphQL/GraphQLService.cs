using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using TradeArt.Contracts.GraphQL;
using TradeArt.Entities;
using TradeArt.Entities.Prices.Response;

namespace TradeArt.Business.GraphQL
{
	public class GraphQLService : IGraphQLService
	{
        private readonly IGraphQLClientFactory _client;
        private const int assetsCount = 100;
        private const int batchSize = 20;

        public GraphQLService(IGraphQLClientFactory client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Assets>> FetchAssetsWithPrices()
        {
            var assets = new List<Assets>();

            var graphQlClient = _client.GetGraphQLClient();

            for (int i = 0; i < assetsCount; i += batchSize)
            {
                var request = new GraphQLRequest
                {
                    Query = @"
                    query PageAssets($skip: Int, $limit: Int) {
                        assets(sort: [{marketCapRank: ASC}], page: {
                        skip: $skip,
                        limit: $limit
                        }) {
                        assetName
                        assetSymbol
                        marketCap
                        markets(filter: {quoteSymbol: {_eq: ""EUR""}}) {
                            marketSymbol
                            ticker {
                                        lastPrice
                                    }
                                }
                        }
                    }",
                    Variables = new
                    {
                        Skip = i,
                        Limit = batchSize
                    }
                };

                var response = await graphQlClient.SendQueryAsync<AssetsWithPricesQuery>(request);

                assets.AddRange(response.Data.Assets);
            }

            return assets;
        }
    }
}

