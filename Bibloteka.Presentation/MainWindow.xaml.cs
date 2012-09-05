using System.Windows;
using Biblioteka.Presentation.Utils;
using Biblioteka.Presentation.ViewModel;
using Biblioteka.Presentation.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;

namespace Biblioteka.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //StyleManager.ApplicationTheme = new MetroTheme();
            InitializeComponent();
            AppMessages.ChangeScreenMessage.Register(this, OnChangeScreenMessage);

        }

        public MainViewModel DataModel
        {
            get { return (MainViewModel) this.DataContext; }
        }

        private void OnChangeScreenMessage(string changeScreen)
        {
            if (childControl.Content != null)
            {
                var clean = (ICleanup) childControl.Content;
                clean.Cleanup();
                childControl.Content = null;
            }
            switch (changeScreen)
            {
                case ViewTypes.BooksView:
                    childControl.Content = new BookView();
                    break;
                case ViewTypes.UsersView:
                    childControl.Content = new UserView();
                    break;
                case ViewTypes.AuthorsView:
                    childControl.Content = new AuthorView();
                    break;
                default :
                    break;
            }
            
        }
        private void ShowLoginIfNotSigned(PropertyChangedMessage<bool> isSigned)
        {
            if(!isSigned.NewValue)
            {
                var login = new LoginView();
                login.ShowDialog();
            }
            
        }
    }
}
