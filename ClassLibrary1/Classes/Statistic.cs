using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    [XmlType("user")]
    public class Statistic
    {
        [XmlElement("login")]
        [JsonProperty("login")]
        public string Login { get; set; }
        [XmlElement("win")]
        [JsonProperty("win")]
        public int Win { get; set; }
        [XmlElement("won")]
        [JsonProperty("won")]
        public int Won { get; set; }
        [XmlElement("nf")]
        [JsonProperty("nf")]
        public int NF { get; set; }
    }
}
