using System;
using System.Diagnostics;
using System.IO;

namespace GithubAutoCommit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists(".git"))
            {
                Console.WriteLine("No Git repository found! Please make sure you are running this exe in the folder that contains the github repository.");
                Console.ReadLine();
            }
            else if (Directory.Exists(".git"))
            {
                GetInfo();
            }
        }
        private static void SendCommits(int commitnum)
        {
            for (int i = 0; i < commitnum; i++)
            {
                var commitprocess = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = "/c cd '" + Environment.CurrentDirectory + "' & git commit --allow-empty -m \"commit " + i + " of " + commitnum + "\" & exit",
                    CreateNoWindow = true,
                };
                Process.Start(commitprocess).WaitForExit();
                Console.WriteLine("Committed " + i + " of " + commitnum);
            }
            Console.WriteLine("Commits finished! Pushing commits...");
            var pushprocess = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = "/c cd '" + Environment.CurrentDirectory + "' & git push & exit",
                CreateNoWindow = false,
            };
            Process.Start(pushprocess).WaitForExit();
            Console.WriteLine("All done! You are good to close this window now.");
            Console.ReadLine();
        }
        private static void GetInfo()
        {
            Console.WriteLine("How many commits would you like to do?");
            int commitnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You input " + commitnum + " number of commits. Is that correct? y/n");
            char uinp = Convert.ToChar(Console.ReadLine());
            if (uinp == 'y')
            {
                SendCommits(commitnum);
            }
            if (uinp == 'n')
            {
                Console.Clear();
                GetInfo();
            }
        }
    }
}
