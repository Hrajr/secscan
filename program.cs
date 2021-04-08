using System;
using System.IO;
using System.Diagnostics;

namespace secscan
{
    public class program
    {
        private static Process proc;
        private static string target;
        private static string assetsDirectory = System.IO.Directory.GetCurrentDirectory().ToString() + @"\assets\";

        static void Main(string[] args)
        {
            Console.WriteLine("Enter target:\r");
            target = setTarget(Console.ReadLine());
            prepareBatchFile();
        }

        private static void prepareBatchFile()
        {
            string cmd = $@"start zap.bat -quickurl {target} -quickout {setFileName()} -quickprogress -cmd";
            File.WriteAllText(assetsDirectory + @"zap\scanner.bat", cmd);
            startScan();
        }

        private static void startScan()
        {
            proc = new Process();
            proc.StartInfo.FileName = assetsDirectory + @"zap\scanner.bat";
            proc.StartInfo.WorkingDirectory = assetsDirectory + "zap";
            proc.Start();
        }

        private static string setTarget(string _target)
        {
            if (_target.Substring(0, 4) != "http")
            {
                return "https://" + _target;
            }
            else return _target;
        }

        private static string setFileName()
        {
            string format = "Mddyyyyhhmmsstt";
            return "\"" + assetsDirectory + @"scans\report" + DateTime.Now.ToString(format) + ".xml\"";
        }
    }
}