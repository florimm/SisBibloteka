using System;
using System.Windows;
using Biblioteka.Presentation.Messages;
using Biblioteka.Presentation.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.ViewModel
{
    public class MainViewModel : ObservableViewModel
    {

        private string _currentScreenText = ViewTypes.BooksView;

        public string CurrentScreenText
        {
            get { return _currentScreenText; }
            private set
            {
                if (!ReferenceEquals(_currentScreenText, value))
                {
                    _currentScreenText = value;
                    RaisePropertyChanged(() => CurrentScreenText);
                }
            }
        }

        private bool _isLoggedIn;

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            private set
            {
                if (value != _isLoggedIn)
                {
                    _isLoggedIn = value;
                    RaisePropertyChanged(() => IsLoggedIn, false, value, true);
                }
            }
        }

        public MainViewModel(IBusy status)
        {
            Messenger.Default.Register<PropertyChangedMessage<IBusy>>(this, _message => Set(_message.NewValue));
            Status = status;
            Status.Busy = true;
            ShutdownCommand = new RelayCommand(Shutdown);
 
            ChangeTab = new RelayCommand<object>(OnChangeScreenCommand);
            MenuClicked = new RelayCommand<string>(OnMenuClicked);
            Status.Busy = false;
        }

        void Set(IBusy busy)
        {
            Status = busy;
        }
        public bool HasChanges
        {
            get
            {
                return false;
            }
        }

        private void OnMenuClicked(string menu)
        {
            Messenger.Default.Send(menu, "menuaction");
        }
        private void OnChangeScreenCommand(object g)
        {
            try
            {
                var tab = (string) g;
                if (CurrentScreenText != tab)
                {
                    if (!HasChanges)
                    {
                        AppMessages.ChangeScreenMessage.Send(tab);//tab is equal with viewTypes
                        CurrentScreenText = tab;
                    }
                    else
                    {
                        // there are pending changes
                        var theResult = MessageBoxResult.Cancel;

                        // ask to confirm canceling any pending changes
                        var dialogMessage = new DialogMessage(this, "",s => theResult = s)
                        {
                            Button = MessageBoxButton.OKCancel,
                            Caption = "Konfirmo"
                        };

                        AppMessages.PleaseConfirmMessage.Send(dialogMessage);

                        if (theResult == MessageBoxResult.OK)
                        {
                            AppMessages.ChangeScreenMessage.Send(tab);//tab is equal with viewTypes
                            CurrentScreenText = tab;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // notify user if there is any error
                AppMessages.RaiseErrorMessage.Send(ex);
            }
        }

        public RelayCommand ButtonCommand { get; private set; }

        public RelayCommand<object> ChangeTab { get; private set; }

        public RelayCommand<string> MenuClicked { get; private set; }


        private RelayCommand _shutdownCommand;
        public RelayCommand ShutdownCommand
        {
            get
            {
                return _shutdownCommand;
            }
            private set
            {
                _shutdownCommand = value;
                RaisePropertyChanged(() => ShutdownCommand);
            }
        }

        private void Shutdown()
        {
            if (ConfirmShutdown())
            {
                App.Current.Shutdown();
            }
        }

        private MessageBoxResult _messageBoxResult;
        private bool ConfirmShutdown()
        {
            var message = new DialogMessage("Close application", p => ShutdownCallback(p));
            message.Caption = "Close message";
            message.Button = MessageBoxButton.OKCancel;
            message.Icon = MessageBoxImage.Question;
            _messageBoxResult = MessageBoxResult.Cancel;
            Messenger.Default.Send<WM>(new WM(this, "viewName"));

            if (_messageBoxResult == MessageBoxResult.OK)
            {
                return true;
            }
            return false;
        }

        private void ShutdownCallback(MessageBoxResult result)
        {
            _messageBoxResult = result;
            if (result == MessageBoxResult.OK)
            {
                SMViewModelLocator.Cleanup();
            }
        }

        public override void Cleanup()
        {
            _shutdownCommand = null;
            base.Cleanup();
        }
    }
}