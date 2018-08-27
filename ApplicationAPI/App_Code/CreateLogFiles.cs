using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for CreateLogFiles
/// </summary>
/// 
namespace CylinderAPI.Log
{
    public class CreateLogFiles
    {
        private string sLogFormat;
        private string sErrorTime;


        public CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
            //File.Create();

        }
        public void ErrorLog(string sErrMsg)
        {
            string fileName = System.Web.HttpContext.Current.Server.MapPath("~/Logs/" + sErrorTime + ".log");

            if (! File.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file
                    Byte[] title = new UTF8Encoding(true).GetBytes(string.Format("\r\n"+ sLogFormat+sErrMsg+ "\r\n"));
                    fs.Write(title, 0, title.Length);
                    
                }
            }
            else
            {
                using (FileStream fs = File.Open(fileName, FileMode.Append))
                {

                    Byte[] title = new UTF8Encoding(true).GetBytes(string.Format("\r\n" + sLogFormat + sErrMsg + "\r\n"));
                    fs.Write(title, 0, title.Length);
                }
            }




        }
    }
}