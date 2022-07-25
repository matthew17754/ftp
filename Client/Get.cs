using System;
using FluentFTP;

namespace Client
{
    public static class Get
    {
        public static int File(ref FtpClient client, Commands.Get files)
        {
            Console.WriteLine("file: " + files.File);

            if (files.Files.Count() > 1)
                return MultipleFiles(files.Files);
            return 0;
        }

        public static int MultipleFiles(IEnumerable<string> files)
        {
            foreach (string file in files)
                Console.WriteLine(file); // remove this
            return 0;
        }

        public static int List(ref FtpClient client, Commands.List directory)
        {
            Console.WriteLine();
            Console.WriteLine("pemdas");
            string path = "./";
            
            try
            {
                if (directory.Local != null)
                {
                    Console.WriteLine("\n\n\nhere\n\n\n");
                    path = Path.GetFullPath(directory.Local);
                }
                else if (directory.Remote != null)
                {
                    Console.WriteLine("\n\n\nyhere\n\n\n");
                    path = "";// Path.GetFullPath(directory.Remote);
                  //  throw new InvalidOperationException("listing remote files (ls -r) not implemented");         // to be implemented!
                }
                else
                {
                    client.GetWorkingDirectory();
                    
                    Console.WriteLine("oof");
                }

                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] sub_directories = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();

                // List sub-directories
                foreach (DirectoryInfo i in sub_directories)
                {
                    Console.WriteLine("{0}/", i.Name);
                }

                if (sub_directories.Length > 0)
                    Console.WriteLine();

                // List files
                foreach (FileInfo j in files)
                {
                    Console.WriteLine(j.Name);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
            return 0;

            /*
            Console.WriteLine("List deez nuts");
            
            try
            {
                FtpListItem[] items = client.GetListing("/Test/two new folders");
                foreach (FtpListItem item in items)
                    Console.WriteLine(item.Name);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
                else
                    Console.WriteLine(ex.Message);
                return -1;
            }
            return 0;*/
        }

        internal static int ChangeDirectory(ref FtpClient client, Commands.ChangeDirectory opts)
        {

            return 0;
        }
    }
}
