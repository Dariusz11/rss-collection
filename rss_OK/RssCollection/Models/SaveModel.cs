using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Models
{
    public class SaveModel
    {
        public string Id { get; set; }
        public string AddressEmail { get; set; }
        public List<string> RssList { get; set; }
    }
}
