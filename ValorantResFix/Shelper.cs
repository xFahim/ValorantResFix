using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ValorantResFix
{
    public class Shelper
    {
        public static int SerializeObj(object obj, string fpath)
        {
            try
            {
                using (FileStream fs = File.Create(fpath))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(fs, obj);

                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static object DeserializeObj(string fpath)
        {
            object obj = null;

            try
            {
                if (File.Exists(fpath))
                {
                    FileStream fs = File.Open(fpath, FileMode.Open);

                    BinaryFormatter bf = new BinaryFormatter();
                    obj = bf.Deserialize(fs);

                    fs.Close();
                }
            }
            catch
            {

            }

            return obj;
        }


    }
}
