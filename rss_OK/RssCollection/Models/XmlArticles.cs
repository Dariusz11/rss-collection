using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssCollection.Models
{
  
    public class XmlArticles
    {
        [XmlElement("item")]
        public List<Article> articles { get; set; }
    }
    [XmlRoot("rss")]
    public class Root
    {
        [XmlElement("channel")]
        public XmlArticles Channel { get; set; }
    }
}
