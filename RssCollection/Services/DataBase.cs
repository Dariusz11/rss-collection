using MongoDB.Driver;
using RssCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public class DataBase : IDataBase
    {
        private readonly IMongoCollection<MailModel> _rss;

        public DataBase(IRssDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _rss = database.GetCollection<MailModel>(settings.CollectionName);
        }

        public bool Create(MailModel mailModel)
        {
            var record = Read(mailModel.MailAddress);
            if (record==null)
            {
                _rss.InsertOne(mailModel);
                return true;
            }
            return false;
 
        }

        public bool Delete(MailModel mailModel)
        {

            throw new NotImplementedException();
        }

        public MailModel Read(string mail)
        {

            var recordCursor=_rss.FindSync(r=>r.MailAddress==mail);

            if (recordCursor.Current==null)
            {
                return null;
            }
            var record = recordCursor.Current.FirstOrDefault();
            return record;

        }

        public bool Update(MailModel mailModel)
        {

            var record = Read(mailModel.MailAddress);
            if (record == null)
            {
                return false;
            }
            record.RssList = mailModel.RssList;
            UpdateDefinition<MailModel> update = Builders<MailModel>.Update.Set(nameof(MailModel.RssList), record.RssList);
            _rss.UpdateOne(r => r.MailAddress == mailModel.MailAddress, update);
            return true;
        }
    }
}
