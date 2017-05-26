using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class JSONWorker<T>:IJSONWorker<T>
    {
        public List<T> GetData(string path)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader file = new StreamReader(new FileStream(path, FileMode.OpenOrCreate)))
                {

                    return (List<T>)serializer.Deserialize(file, typeof(List<T>));

                }
            }
            catch
            {
                return new List<T>();
            }

        }

        public void WriteData(List<T> objects, string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter file = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
            {
                serializer.Serialize(file, objects);
            }

        }
    }
}
