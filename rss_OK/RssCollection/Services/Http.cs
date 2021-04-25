using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public class Http : IHttp
    {
        public async Task<string> GetString(string link)
        {
            HttpClient httpClient = new HttpClient(); 
            string xmlCode = await httpClient.GetStringAsync(link);
            return xmlCode;
        }
    }
}
