using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public interface IHttp
    {
        Task<string> GetString(string link);
    }
}
