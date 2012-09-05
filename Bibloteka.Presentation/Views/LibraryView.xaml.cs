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
    public partial class LibraryView : UserControl, ICleanup
    {
        public LibraryView()
        {
            InitializeComponent();
        }

        public void Cleanup()
        {
            ((ICleanup)this.LayoutRoot.DataContext).Cleanup();
        }
    }
}
