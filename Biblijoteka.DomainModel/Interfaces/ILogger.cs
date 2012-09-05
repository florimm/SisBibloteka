using System;

namespace Biblioteka.DomainModel.Interfaces
{
    public interface ILogger
    {
        void Trace(string _message);
        void Debug(string _message);
        void Info(string _message);
        void Warn(string _message);
        void Error(string _message);
        void Fatal(string _message);

        void TraceLine(string _message);
        void DebugLine(string _message);
        void InfoLine(string _message);
        void WarnLine(string _message);
        void ErrorLine(string _message);
        void FatalLine(string _message);

        void Trace(Exception ex, string _message);
        void Debug(Exception ex, string _message);
        void Info(Exception ex, string _message);
        void Warn(Exception ex, string _message);
        void Error(Exception ex, string _message);
        void Fatal(Exception ex, string _message);
    }
}