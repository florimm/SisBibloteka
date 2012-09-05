using System;
using System.Windows;
using System.Windows.Controls;
using Biblioteka.Presentation.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.Views
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookView : UserControl, ICleanup
    {
        public BookView()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "DialogBook", ShowView);
        }

        public BookViewModel ViewModel
        {
            get { return (BookViewModel) LayoutRoot.DataContext; }
        }

        private void ShowView(string action)
        {
            if (action == "hide")
            {
                editControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                editControl.Visibility = Visibility.Visible;
            }
        }

        public void Cleanup()
        {
            Messenger.Default.Unregister<string>(this, "DialogBook", ShowView);
            ((ICleanup)LayoutRoot.DataContext).Cleanup();
        }

        private void OkClick(object _sender, EventArgs _e)
        {
            ViewModel.OK.Execute(null);
        }
        private void CancelClick(object _sender, EventArgs _e)
        {
            ViewModel.selected = null;
            ShowView("hide");
        }
    }
}
