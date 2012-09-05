using System;
using System.Windows;
using Biblioteka.Presentation.Utils;
using Biblioteka.Presentation.ViewModel;
using Biblioteka.Presentation.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using StructureMap;
using Telerik.Windows.Controls;

namespace Biblioteka.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //StyleManager.ApplicationTheme = new MetroTheme();
            //IContainer container = ViewModelBase.IsInDesignModeStatic ? Bootstrapper.InitializeForBlend() : Bootstrapper.Initialize();
            //App.Current.Resources.Add("Locator", new SMViewModelLocator());
            //var lv = new LoginView();
            
        }
        static App()
        {
            //
            Bootstrapper.Initialize();
            DispatcherHelper.Initialize();
        }

    }
}
