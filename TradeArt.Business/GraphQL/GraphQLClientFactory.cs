using System;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using TradeArt.Contracts.GraphQL;

namespace TradeArt.Business.GraphQL
{
	public class GraphQLClientFactory : IGraphQLClientFactory
	{
        private readonly string _baseurl;

        public GraphQLClientFactory(string baseurl)
        {
            _baseurl = baseurl;
        }

        public IGraphQLClient GetGraphQLClient()
        {
            return new GraphQLHttpClient(_baseurl, new NewtonsoftJsonSerializer());
        }
	}
}

