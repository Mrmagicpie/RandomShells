using System;
using System.IO;
using System.Text;

namespace CSharpShell.Commands
{
    public class BuiltIn
    {
        public static void HostnameCTL()
        {
            if (Global.INPUT.StartsWith("hostnamectl --help"))
            {
                var help = new StringBuilder()
                    .Append(" \n")
                    .Append("HostnameCTL Help!")
                    .Append(" \n")
                    .Append("You will be asked for each option!")
                    .Append(" \n")
                    .Append("set   - Set your shell hostname to something.")
                    .Append("reset - Reset your shell's hostname.")
                    .Append(" \n");

                Console.WriteLine(help);
            }
            
            Console.Write("Please state what you'd like to do(use 'hostnamectl --help' for help): ");
            var kek = Console.ReadLine();
            
            if (string.IsNullOrEmpty(kek) || string.IsNullOrWhiteSpace(kek))
            { Console.WriteLine("Not a valid option!"); }
            
            if (kek.StartsWith("reset"))
            {
                Console.WriteLine("Resetting your hostname!");
                Global.HOSTNAME = Environment.MachineName;
            }
            
            if (kek.StartsWith("set"))
            {
                Console.Write("What would you like to set your Hostname to? ");
                var hostname = Console.ReadLine();
                if (string.IsNullOrEmpty(hostname) || string.IsNullOrWhiteSpace(hostname))
                { Console.WriteLine("Not a valid hostname!"); }
                else
                {
                    Global.HOSTNAME = hostname;
                    Console.WriteLine("Set your hostname to {0}", Global.HOSTNAME);
                }
            }
            else
            { Console.WriteLine("Not a valid option!"); }
        }

        public static void LS()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo[] dirs = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            
            foreach (DirectoryInfo dir in dirs)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0,-25} {1,25}", dir.FullName, dir.LastWriteTime);
            }
            
            foreach (FileInfo file in files)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{0,-25} {1,25}", file.Name, file.LastWriteTime);
            }
        }
        
        private static void FileNotFound(string path)
        {
            Console.WriteLine();
            Console.WriteLine("The file or directory can't be found!");
            Console.WriteLine("File/Directory: {0}", path);
            Console.WriteLine();
        }

        public static void CD()
        {
            var cd = Global.INPUT.Replace("cd", "");
            
            if (cd.StartsWith(" "))
            { cd = cd.Replace(" ", ""); }
            
            if (string.IsNullOrWhiteSpace(cd))
            {
                if (Global.CDHOME) 
                { Directory.SetCurrentDirectory(Global.HOME); }
                else 
                { Console.WriteLine("Please specify somewhere to cd into!"); }
            }
            
            else
            {
                try
                {
                    // TODO: Making this easier to cd into subdirs from home dir
                    if (cd.StartsWith("~"))
                    { Directory.SetCurrentDirectory(Global.HOME); }
                    else
                    { Directory.SetCurrentDirectory(cd); }
                }
                catch (FileNotFoundException)
                { FileNotFound(cd); }
                catch (DirectoryNotFoundException)
                { FileNotFound(cd); }
                catch (Exception e)
                {
                    Console.WriteLine("An error has occured!");
                    Console.WriteLine("Error: {0}", e);
                }
            }
        }
    }
}