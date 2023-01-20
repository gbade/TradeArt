using System;
using System.Collections.Generic;
using TradeArt.Entities;
using System.Threading.Tasks;

namespace TradeArt.Contracts.GraphQL
{
	public interface IGraphQLService
	{
        Task<IEnumerable<Assets>> FetchAssetsWithPrices();
    }
}

