using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmojiNamer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            foreach (var sub in Directory.EnumerateDirectories(path))
            {
                //string[] files = Directory.GetFiles(sub);
                int i = 0;
                foreach (var file in Directory.EnumerateFiles(sub))
                {
                    File.Move(file, Path.Combine(sub, $"{i++}.png"));
                }
            }
        }
    }
}
