using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public interface IMailing
    {
        bool Sending(string Receiver, string Topic, string Description);

        
    }
}
