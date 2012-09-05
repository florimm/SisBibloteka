using System;
using System.Net;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.DomainModel.DomainDefaultImplementation
{
    public class OutputLogger : ILogger
    {

        public void InfoLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Info " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }


        public void Trace(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Trace");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Debug(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Debug");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Info(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Info");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Warn(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Warn");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Error(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Error");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Fatal(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : " + _message);
                System.Diagnostics.Debug.WriteLine("Type        : Fatal");
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void TraceLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Trace " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void DebugLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Debug " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void WarnLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Warn " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void ErrorLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Error " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void FatalLine(string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Fatal " + _message + " " + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Trace(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Trace " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Debug(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Debug " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Info(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Info " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Warn(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Warn " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Error(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Error " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Fatal(Exception ex, string _message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Message     : Fatal " + _message);
                System.Diagnostics.Debug.WriteLine("Source      : " + ex.Source.Trim());
                System.Diagnostics.Debug.WriteLine("Method      : " + ex.TargetSite.Name);
                System.Diagnostics.Debug.WriteLine("Time        : " + DateTime.Now.ToLongTimeString());
                System.Diagnostics.Debug.WriteLine("Date        : " + DateTime.Now.ToShortDateString());
                System.Diagnostics.Debug.WriteLine("Computer    : " + Dns.GetHostName());
                System.Diagnostics.Debug.WriteLine("Error       : " + ex.Message.Trim());
                System.Diagnostics.Debug.WriteLine("Stack Trace : " + ex.StackTrace.Trim());
                System.Diagnostics.Debug.WriteLine(
                    "^^-------------------------------------------------------------------^^");
                System.Diagnostics.Debug.Flush();
                System.Diagnostics.Debug.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
