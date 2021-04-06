using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RssCollection.Models;
using RssCollection.Services;

namespace RssCollection.Controllers
{
    [Route("api/rss")]
    [ApiController]


    public class RssController : ControllerBase
    {
        private IDataBase dataBase;
        private IMailing mailing;
        private IHttp httpClient;

        public RssController(IDataBase dataBase, IMailing mailing, IHttp httpClient)
        {
            this.dataBase = dataBase;
            this.mailing = mailing;
            this.httpClient = httpClient;
        }


        private async Task<string> GetHtml(string addresEmail)
        {
            var record = dataBase.Read(addresEmail);
            if (record == null)
            {
                return null;
            }

            string html = @"
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
</head>
<body>
";
            //record.RssList
            for (int i = 0; i < record.RssList.Count; i++)
            {
                string link = record.RssList[i];
                string xmlCode = await httpClient.GetString(link);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Root));
                
                using (TextReader textReader = new StringReader(xmlCode)) {
                    XmlArticles xmlArticles = ((Root)xmlSerializer.Deserialize(textReader)).Channel;
                    //ładowane teksty pobrane z linków
                    for (int j = 0; i < xmlArticles.articles.Count; i++)
                    {
                        html += "</br></br>";
                        html += xmlArticles.articles[j].Title;
                        html += "</br>";
                        html += xmlArticles.articles[j].Description;
                    }
                };

            }

            html += "</body></html >";
            return html;
        }


        

        [Route("preview")]
        public async Task<IActionResult> Preview([FromQuery] string addresEmail)
        {
            if (addresEmail==null || addresEmail=="")
            {
                return BadRequest("Błąd parametru");
            }
            string html = await GetHtml(addresEmail);
           if (html==null)
            {
                return BadRequest("Rekord nie został znaleziony");
            }
            return Ok(html);
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody]Models.SendModel sendModel)
        {
            if (sendModel.AddressEmail == null || sendModel.AddressEmail == "")
            {
                return BadRequest("Błąd parametru");
            }
            string html = await GetHtml(sendModel.AddressEmail);
            if (html == null)
            {
                return NotFound("Rekord nie został znaleziony");
            }
            mailing.Sending(sendModel.AddressEmail, "Newsletter", html);
            return Ok("");


        }


    }
}