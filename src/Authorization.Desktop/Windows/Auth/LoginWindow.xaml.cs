using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.Pages.Auth;
using Authorization.Desktop.Repositories.Users;
using Authorization.Desktop.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Authorization.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserRepository _repository;
        private string regUsername { get; set; }
        private string regPassword { get; set; }
        private string passwordShow { get; set; }

        public LoginWindow()
        {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            PageNavigator.Content = loginPage;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }        
    }
}
