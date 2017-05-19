using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Krestiki_Noliki.Classes.Statistics
{
    public class XmlWorker<T> : IXmlWorker<T>
    {
        public List<T> GetData(string path)
        {
            List<T> statistics = new List<T>();
            XmlSerializer xml = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    statistics = xml.Deserialize(stream) as List<T>;
                }
                catch
                {
                    return new List<T>();
                }
            }
            return statistics;
           
        }

        public void WriteData(string path, List<T> Data)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(path, FileMode.Truncate))
            {
                xml.Serialize(stream,Data);
            }
        }
    }
}
