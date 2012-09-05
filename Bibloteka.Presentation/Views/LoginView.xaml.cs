using System.Windows;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using StructureMap;

namespace Biblioteka.Presentation.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            
        }

        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            if(usernameTextBox.Text.Length < 3 && passwordTextBox.Password.Length > 3)
            {
                
            }
            else
            {
                var repo = ObjectFactory.GetInstance<IUserRepository>();
                User usr = repo.GetByUserAndPassword(usernameTextBox.Text, passwordTextBox.Password);
                if(usr != null)
                {
                    var frm = new MainWindow();
                    frm.Show();
                    
                    this.Close();
                }

            }
                
        }

        private void btnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
