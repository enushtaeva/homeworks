using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Krestiki_Noliki.Classes.Statistics
{
    [XmlType("user")]
    public class Statistic
    {
        [XmlElement("login")]
        public string Login { get; set; }
        [XmlElement("win")]
        public int Win { get; set; }
        [XmlElement("won")]
        public int Won { get; set; }
        [XmlElement("nf")]
        public int NF { get; set; }
    }
}
