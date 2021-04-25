using RssCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public interface IDataBase
    {
        bool Create(MailModel mailModel);
        MailModel Read(string mail);
        bool Update(MailModel mailModel);
        bool Delete(MailModel mailModel);

    }
}
