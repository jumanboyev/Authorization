using Authorization.Desktop.Entities.Users;
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
            this._repository = new UserRepository();
            if (Properties.Settings.Default.RememberMe)
            {
                txtPassword.Password = Properties.Settings.Default.Password;
                txtUsername.Text = Properties.Settings.Default.Username;
                chremember.IsChecked = true;
            }
            else
            {
                chremember.IsChecked = false;
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Visible)
            {
                noteye.Visibility = Visibility.Visible;
                eye.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                txbParol.Visibility = Visibility.Visible;
                txbParol.Text = txtPassword.Password;
            }
            else
            {
                noteye.Visibility = Visibility.Hidden;
                eye.Visibility = Visibility.Visible;

                txtPassword.Visibility = Visibility.Visible;
                txbParol.Visibility = Visibility.Hidden;
                txtPassword.Password = txbParol.Text;
            }
        }

        public bool ContainsNonLatinCharacters(string input)
        {
            string latinAlphaNumericPattern = @"^[a-zA-Z0-9]+$";
            // Check if the input contains any Latin alphabets
            return Regex.IsMatch(input, latinAlphaNumericPattern);
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Visible) passwordShow = txtPassword.Password;
            else passwordShow = txbParol.Text;

            int count = 0;
            if (ContainsNonLatinCharacters(txtUsername.Text) == true && ContainsNonLatinCharacters(passwordShow) == true) count++;
            else MessageBox.Show("Faqat lotin alifbosidan foydalaning");

            if (txtUsername.Text.Length >= 8) count++;
            else MessageBox.Show("Ismda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin");

            if (txtPassword.Password.Length >= 8) count++;
            else MessageBox.Show("Parolda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin");

            var dbResult = await _repository.GetByIdUserName(txtUsername.Text);
            if (dbResult.Count == 1)
            {               
                string passwordHash = dbResult[0].Password;
                string salt = dbResult[0].Salt;
                string password = txtPassword.Password;
                var passwordcheck = PasswordHasher.Verify(password, passwordHash, salt);
                if (passwordcheck is true) count++;
                else MessageBox.Show("Parol noto'g'ri");
            }
            else
            {
                MessageBox.Show("Bunday foydalanuvchi mavjud emas!");
            }

            if (count == 4)
            {
                if (chremember.IsChecked == true)
                {
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Password = passwordShow;
                    Properties.Settings.Default.Username = txtUsername.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.Save();
                }
                MainWindow window = new MainWindow();
                this.Close();
                window.ShowDialog();

            }
        }

        public void setData(string userName, string userPassword)
        {
            regUsername = userName;
            regPassword = userPassword;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = txtUsername.Text + e.Text;

            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    e.Handled = true; // Block input if it is not a letter or digit, or if it is whitespace
                    break;
                }
            }

        }
        private void btnRoyxatdanOtish_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            this.Close();
            window.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (regUsername != null && regPassword != null)
            {
                txtUsername.Text = regUsername;
                txtPassword.Password = regPassword;
            }
        }
    }
}
