using System;
using GraphQL.Client.Abstractions;

namespace TradeArt.Contracts.GraphQL
{
	public interface IGraphQLClientFactory
	{
        IGraphQLClient GetGraphQLClient();
    }
}

