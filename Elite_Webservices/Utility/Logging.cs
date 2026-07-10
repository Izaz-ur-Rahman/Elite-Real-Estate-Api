using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Elite_Webservices.Utility
{
    public class Logging
    {
        public Logging()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void WriteLog(LogType logType, string text)
        {
            try
            {
                string LogPath = ConfigurationManager.AppSettings["LogPath"];
                string file = AppDomain.CurrentDomain.BaseDirectory + LogPath;
                string dir = file.Substring(0, file.LastIndexOf(@"\"));
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);

                FileStream fStream;
                if (File.Exists(file))
                    fStream = File.Open(file, FileMode.Append, FileAccess.Write);
                else
                    fStream = File.Open(file, FileMode.CreateNew, FileAccess.ReadWrite);
                StreamWriter objStreamWriter = new StreamWriter(fStream, System.Text.Encoding.UTF8);
                objStreamWriter.WriteLine("Start =========================================================================== Start");
                objStreamWriter.WriteLine(logType.ToString() + " | " + DateTime.Now.ToString() + " | " + text);
                objStreamWriter.WriteLine("End   ===========================================================================   End");
                objStreamWriter.Close();
                objStreamWriter.Dispose();
            }
            catch (Exception ex) { }

        }
    }
    public enum LogType
    {
        Error = 1,
        Warning = 2,
        Info = 3,
        Critical = 4
    }
}