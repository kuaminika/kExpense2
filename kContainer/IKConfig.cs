using System;
using System.Collections.Generic;
using System.Text;

namespace kContainer
{
    public interface IKServiceConfig
    {
        int orgId { get; }
        string connectionString { get;  }
    }
}
