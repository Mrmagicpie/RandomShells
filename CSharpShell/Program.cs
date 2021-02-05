using System;
using System.Text;
using CSharpShell.Commands;
using System.IO;

namespace CSharpShell
{
    /// <summary>
    /// Class for all Shell wide variables.
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Set a Shell variable for the users home dir. Readonly because, yea.
        /// </summary>
        public static readonly string   HOME      = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        /// <summary>
        /// Shell variable for the hostname. Not readonly so you can change it per session.
        /// </summary>
        public static string            HOSTNAME  = Environment.MachineName;
        
        /// <summary>
        /// Readonly variable similar to the home dir because you only have one user per shell.
        /// </summary>
        public static readonly string   USER      = Environment.UserName;
        
        /// <summary>
        /// Current directory.
        /// </summary>
        public static string            PWD       = Directory.GetCurrentDirectory();
        
        /// <summary>
        /// Shell variable to change the way cd works.
        /// </summary>
        public static bool              CDHOME    = true;
        
        /// <summary>
        /// Command input.
        /// </summary>
        public static string            INPUT;
    }

    /// <summary>
    /// Main Shell Class. Contains everything needed to handle basic commands.
    /// </summary>
    public class CSharpShell
    {
        /// <summary>
        /// Main Shell function. Starts a Shell that doesn't end.
        /// </summary>
        /// <returns>"Never" ending Shell.</returns>
        public static void Main()
        {
            if (Global.CDHOME)
            { Directory.SetCurrentDirectory(Global.HOME); }
            
            while (true)
            {
                Global.PWD = Directory.GetCurrentDirectory();
                
                if (Global.PWD.ToLower() == Global.HOME.ToLower())
                { Global.PWD = "~"; }
                else 
                { Global.PWD = Global.PWD.Replace(Global.HOME, "~"); }
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("{0}@{1} [ {2} ]: $ ", Global.USER, Global.HOSTNAME, Global.PWD);
                
                Global.INPUT = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Global.INPUT)) { continue; }
                if (string.IsNullOrEmpty(Global.INPUT)) { continue; }
                if (Global.INPUT.ToLower() == "exit") { Console.WriteLine("\nExiting the shell!\n"); break; }
                else { Command(); }
            }
        }

        // Yes ik it can be private but if somehow someone wants to use this they can
        
        /// <summary>
        /// Command handler. Deals with all Shell commands.
        /// </summary>
        /// <returns>Calls a function based on the command given.</returns>
        public static void Command()
        {
            // Bool to stop the command function. I don't really know why its executing the if and else. Probably
            //                            because I'm using a global variable.
            bool running = true;
            
            // BuiltIn commands.
            if (Global.INPUT.ToLower().StartsWith("hostnamectl"))
            {
                running = false;
                BuiltIn.HostnameCTL();
            }
            if (Global.INPUT.StartsWith("ls"))
            {
                running = false;
                BuiltIn.LS();
            }
            if (Global.INPUT.StartsWith("cd"))
            {
                running = false;
                BuiltIn.CD();
            }
            if (Global.INPUT.StartsWith("clear"))
            {
                running = false;
                Console.Clear();
            }
            if (Global.INPUT.StartsWith("help"))
            {
                running = false;
                var help = new StringBuilder()
                    .Append(" \n")
                    .Append("Mrmagicpie C# Shell Help!")
                    .Append(" \n")
                    .Append("Work in progress!")
                    .Append(" \n");
                Console.WriteLine(help);
            }
            
            // For everything else
            // TODO: Create a way to add commands in external classes.
            else
            {
                if (running)
                {
                    var none = new StringBuilder()
                        .Append(" \n")
                        .Append("Unknown command! Use \"help\" to get help!")
                        .Append(" \n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(none);
                }
            }
        }
    }
}