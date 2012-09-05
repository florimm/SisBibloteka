using System;
using System.IO;
using System.Windows.Media.Imaging;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.Utils
{
    public static class AppMessages
    {
        enum MessageTypes
        {
            // MainPage View messages
            ChangeScreen,
            ChangeScreenNoAnimation,
            RaiseError,
            PleaseConfirm,
            StatusUpdate,
            // BookEditorViewModel messages
            EditBook,
            // BookEditor View message
            ReadOnlyBook,
            OpenFile,
            // Reports View message
            GetCharts
        }

        public static class ChangeScreenMessage
        {
            public static void Send(string screenName)
            {
                Messenger.Default.Send(screenName, MessageTypes.ChangeScreen);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.ChangeScreen, action);
            }
        }

        public static class ChangeScreenNoAnimationMessage
        {
            public static void Send(string screenName)
            {
                Messenger.Default.Send(screenName, MessageTypes.ChangeScreenNoAnimation);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.ChangeScreenNoAnimation, action);
            }
        }

        public static class RaiseErrorMessage
        {
            public static void Send(Exception ex)
            {
                Messenger.Default.Send(ex, MessageTypes.RaiseError);
            }

            public static void Register(object recipient, Action<Exception> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.RaiseError, action);
            }
        }

        public static class PleaseConfirmMessage
        {
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send(dialogMessage, MessageTypes.PleaseConfirm);
            }

            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.PleaseConfirm, action);
            }
        }

        public static class StatusUpdateMessage
        {
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send(dialogMessage, MessageTypes.StatusUpdate);
            }

            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.StatusUpdate, action);
            }
        }

        public static class EditBookMessage
        {
            public static void Send(Book book)
            {
                Messenger.Default.Send(book, MessageTypes.EditBook);
            }

            public static void Register(object recipient, Action<Book> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.EditBook, action);
            }
        }

        public static class ReadOnlyIssueMessage
        {
            public static void Send(Boolean readOnly)
            {
                Messenger.Default.Send(readOnly, MessageTypes.ReadOnlyBook);
            }

            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.ReadOnlyBook, action);
            }
        }

        public static class OpenFileMessage
        {
            public static void Send(NotificationMessageAction<FileInfo> message)
            {
                Messenger.Default.Send(message, MessageTypes.OpenFile);
            }
            public static void Register(object recipient, Action<NotificationMessageAction<FileInfo>> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.OpenFile, action);
            }
        }

        public static class GetChartsMessage
        {
            public static void Send(NotificationMessageAction<WriteableBitmap> message)
            {
                Messenger.Default.Send(message, MessageTypes.GetCharts);
            }
            public static void Register(object recipient, Action<NotificationMessageAction<WriteableBitmap>> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.GetCharts, action);
            }
        }

        public static class ChangeScreenMenu
        {
            public static void Send(string screenName, string screenAction)
            {
                Messenger.Default.Send(screenName, MessageTypes.ChangeScreen);
            }

            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register(recipient, MessageTypes.ChangeScreen, action);
            }
        }
    }

    public class Current : ICurrent
    {
        public User CurrentUser
        {
            get; 
            set;
        }
    }
}
