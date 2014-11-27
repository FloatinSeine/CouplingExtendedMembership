using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupling.Domain.Model.Membership;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Coupling.Domain.Persistence.Raven.Indexes
{
    public class NextUserIdIndex : AbstractIndexCreationTask<Account, NextUserIdIndex.QueryResult>
    {
        public class QueryResult
        {
            public int UserId { get; set; }
        }
        public class Result
        {
            public int NextId { get; set; }
        }

        public NextUserIdIndex()
        {
            Map = accounts => from account in accounts
                              select new QueryResult
                              {
                                  UserId = account.UserId
                              };

            Index(x => x.UserId, FieldIndexing.NotAnalyzed);
            StoreAllFields(FieldStorage.Yes);

            Reduce = results => from result in results
                                group result by 0 into g
                                select new
                                {
                                    NextId = g.Max(x => x.UserId)
                                };
        }
    }
}
