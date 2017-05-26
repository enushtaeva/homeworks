using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IJSONWorker<T>
    {
         List<T> GetData(string path);
         void WriteData(List<T> objects, string path);
    }
}
