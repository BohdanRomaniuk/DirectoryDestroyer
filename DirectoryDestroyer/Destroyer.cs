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
        static void DeleteFiles(DirectoryInfo directory)
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
            string directoriesPaths = "Directories.txt";
            if (args!=null && args.Length!=0)
            {
                string directoryPath = args[0];
                ConsoleKey response;
                do
                {
                    Console.Write("Are you sure you want to delete {0}? [y/n] ", directoryPath);
                    response = Console.ReadKey(false).Key;
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                bool confirmed = (response == ConsoleKey.Y);
                if(confirmed)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        DeleteFiles(new DirectoryInfo(Path.GetFullPath(directoryPath)));
                    }
                    catch(Exception pathExcpt)
                    {
                        Console.WriteLine($"Path error! {pathExcpt.Message}");
                    }
                }
            }
            else if(File.Exists(directoriesPaths) && new FileInfo(directoriesPaths).Length != 0)
            {
                string[] paths = File.ReadAllLines(directoriesPaths);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Directories to delete:");
                Console.ForegroundColor = ConsoleColor.White;
                foreach(string path in paths)
                {
                    Console.WriteLine(path);
                }
                ConsoleKey response;
                do
                {
                    Console.Write("Are you sure you want to delete all this directories? [y/n] ");
                    response = Console.ReadKey(false).Key;
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                bool confirmed = (response == ConsoleKey.Y);
                if (confirmed)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (string path in paths)
                        {
                            DeleteFiles(new DirectoryInfo(Path.GetFullPath(path)));
                        }
                    }
                    catch (Exception pathExcpt)
                    {
                        Console.WriteLine($"Path error! {pathExcpt.Message}");
                    }
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No arguments given in command line and Directories.txt file is empty");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
