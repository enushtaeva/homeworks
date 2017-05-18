using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Krestiki_Noliki.Classes.Statistics
{
    [XmlRootAttribute("User",IsNullable=false)]
    public class Statistic
    {
        public string Login { get; set; }
        public int Win { get; set; }
        public int Won { get; set; }
        public int NF { get; set; }
    }
}
