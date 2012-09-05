using System;

namespace Biblioteka.Presentation.Utils
{
    public interface IUIWindowDialogService
    {
        bool? ShowDialog(string titel, object datacontext);
    }

    public interface IDialogResultVMHelper
    {
        event EventHandler DialogResultTrueEvent;
    }

    public class WpfUIWindowDialogService : IUIWindowDialogService
    {
        public bool? ShowDialog(string titel, object datacontext)
        {
            var win = new WindowDialog {Title = titel, DataContext = datacontext};

            return win.ShowDialog();
        }

    }
}