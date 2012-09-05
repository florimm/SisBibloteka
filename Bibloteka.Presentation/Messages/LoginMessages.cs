using System;
using System.Windows;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.Messages
{
    public class DialogWindowMessage : NotificationMessageAction<MessageBoxResult>
    {
        public DialogWindowMessage(object sender, Action<MessageBoxResult> callback)
            : base(sender, "GetPassword", callback)
        {

        }
    }

    public class WM : GenericMessage<string>
    {
        public WM(object sender, string viewName)
            : base(sender, viewName)
        {
            
        }
        public void ProcessCallback(MessageBoxResult result)
        {
            if (this.Callback == null)
                return;
            this.Callback(result);
        }

        public Action<MessageBoxResult> Callback { get; private set; }
    }
}
