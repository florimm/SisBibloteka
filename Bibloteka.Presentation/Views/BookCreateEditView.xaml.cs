using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Biblioteka.Presentation.Utils;
using Biblioteka.Presentation.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Biblioteka.Presentation.Views
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookCreatEditView : UserControl, ICleanup
    {
        public BookCreatEditView()
        {
            InitializeComponent();
        }

        public event EventHandler OkClick;
        public event EventHandler CancelClick;

        public void Cleanup()
        {
            ((ICleanup)this.LayoutRoot.DataContext).Cleanup();
        }
        private void buttonCancel_click(object sender, RoutedEventArgs e)
        {
            if (CancelClick != null)
                CancelClick(this, e);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (OkClick != null)
                OkClick(this, e);
        }
    }
}
