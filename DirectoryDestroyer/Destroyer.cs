using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DD
{
    class DirectoryDestroyer
    {
        public static void DeleteFiles(DirectoryInfo directory)
        {
            if (directory.GetDirectories() != null && directory.GetDirectories().Length != 0)
            {
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    DeleteFiles(subDirectory);
                }
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
                file.Delete();
                if (!File.Exists(file.FullName))
                {
                    Console.WriteLine(file.FullName + " DELETED!");
                }
            }
            directory.Attributes = FileAttributes.Normal;
            directory.Delete();
            if (!Directory.Exists(directory.FullName))
            {
                Console.WriteLine(directory.FullName + " DELETED!");
            }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.ReadKey();
            DeleteFiles(new DirectoryInfo("D:/Тест"));
            Console.ReadKey();
        }
    }
}
