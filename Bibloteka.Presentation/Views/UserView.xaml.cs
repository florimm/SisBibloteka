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
using GalaSoft.MvvmLight;

namespace Biblioteka.Presentation.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl, ICleanup
    {
        public UserView()
        {
            InitializeComponent();
        }

        public void Cleanup()
        {
            ((ICleanup)this.LayoutRoot.DataContext).Cleanup();
        }
    }
}
