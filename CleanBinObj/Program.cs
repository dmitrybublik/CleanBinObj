using System;
using System.IO;

namespace CleanBinObj
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentPath = Directory.GetCurrentDirectory();

            if (args.Length == 1)
            {
                currentPath = args[0];
            }

            if (!Directory.Exists(currentPath))
            {
                Console.WriteLine("Invalid path '{0}'.", currentPath);
                return;
            }

            Console.WriteLine("Process path '{0}'?", currentPath);
            var retVal = Console.ReadKey();

            if (retVal.KeyChar != 'y' && retVal.KeyChar != 'Y')
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine();

            ProcessPath(currentPath);
        }

        private static void WriteLine(int depth, string format, params object[] args)
        {
            for (int i = 0; i < depth; ++i)
            {
                Console.Write(' ');
            }

            Console.WriteLine(format, args);
        }

        private static bool FileNamesAreEqual(string name1, string name2)
        {
            return string.Compare(name1, name2, StringComparison.OrdinalIgnoreCase) == 0;
        }

        private static void ProcessPath(string path, int depth = 0)
        {
            WriteLine(depth, "Process folder '{0}'.", path);
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                if (FileNamesAreEqual(name, "bin") || FileNamesAreEqual(name, "obj"))
                {
                    WriteLine(depth, "Delete folder '{0}'…", folder);
                    Directory.Delete(folder, true);
                    WriteLine(depth, "Ok.");
                    continue;
                }
                ProcessPath(folder);
            }
        }
    }
}
