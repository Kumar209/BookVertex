using System;
using System.Collections.Generic;
using System.Text;

namespace BookVertex.DataAccess.DbInitializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
