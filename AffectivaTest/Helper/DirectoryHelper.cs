using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRUTWeb
{
    public class DirectoryHelper
    {
        public static int CountFile(string path) =>
            Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length;

        public static int RandomFile(string path)
        {
            Random rand = new Random();
            return rand.Next(CountFile(path));
        }
    }
}
