using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RssCollection.Models;
using RssCollection.Services;

namespace RssCollection.Controllers
{

    [Route("api/database")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        private IDataBase dataBase;

        public DataBaseController(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody]Models.SaveModel rssModel)
        {

            //1.znalezienie rekordu
            
            var record = dataBase.Read(rssModel.AddressEmail);
            //2.sprawdzenie czy jest nullem

            if (record == null)
            {

                dataBase.Create(new MailModel { MailAddress= rssModel.AddressEmail, RssList=rssModel.RssList});
            }
             
            //2.1.jeżeli tak to dodanie nowego rekordu z danymi z argumentu typu SaveModel
            //2.2.jeżeli nie to aktualizacja rekordu danymi z argumentu typu SaveModel
            else
            {
                record.RssList = rssModel.RssList;
                dataBase.Update(record);
            }

            return Ok();
        }
    }
}