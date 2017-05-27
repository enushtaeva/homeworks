using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    [XmlType("statistic")]
    public  class StatisticOnTask
    {
        private TimeSpan m_TimeSinceLastEvent;
        [JsonProperty("dateofstart")]
        [XmlElement("datefstart")]
        public DateTime DateOfStart { get; set; }
        [JsonProperty("result")]
        [XmlElement("result")]
        public int Result { get; set; }
        [JsonProperty("figure")]
        [XmlElement("figure")]
        public int X { get; set; }
        [JsonProperty("countofstep")]
        [XmlElement("cuntofstep")]
        public int CountOfStep { get; set; }
        [XmlIgnore]
        [JsonProperty("timetoplay")]
        public TimeSpan TimeToPlay//- это длительность игры
        {
            get { return m_TimeSinceLastEvent; }
            set { m_TimeSinceLastEvent = value; }
        }

        // Pretend property for serialization
        [XmlElement("timetoplay")]
        public long TimeSinceLastEventTicks
        {
            get { return m_TimeSinceLastEvent.Ticks; }
            set { m_TimeSinceLastEvent = new TimeSpan(value); }
        }
    }
}
