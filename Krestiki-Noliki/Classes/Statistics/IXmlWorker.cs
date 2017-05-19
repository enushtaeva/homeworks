using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krestiki_Noliki.Classes.Statistics
{
    public interface IXmlWorker<T>
    {
         List<T> GetData(string path);
         void WriteData(string path, List<T> Data);
    }
}
