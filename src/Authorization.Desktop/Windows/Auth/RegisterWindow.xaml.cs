using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Interfaces.Users;
using Authorization.Desktop.Repositories.Users;
using Authorization.Desktop.Security;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Authorization.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly IUserRepository _repository;
        public RegisterWindow()
        {
            InitializeComponent();
            this._repository = new UserRepository();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnShaxsiykabinet_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            this.Close();
            window.ShowDialog();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtRegPassword.Visibility == Visibility.Visible)
            {
                noteye.Visibility = Visibility.Visible;
                eye.Visibility = Visibility.Hidden;

                txtRegPassword.Visibility = Visibility.Hidden;
                txbParol.Visibility = Visibility.Visible;
                txbParol.Text = txtRegPassword.Password;


            }
            else
            {
                noteye.Visibility = Visibility.Hidden;
                eye.Visibility = Visibility.Visible;

                txtRegPassword.Visibility = Visibility.Visible;
                txbParol.Visibility = Visibility.Hidden;
                txtRegPassword.Password = txbParol.Text;
            }
        }

        private void eyecon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtRegPasswordcon.Visibility == Visibility.Visible)
            {
                noteyecon.Visibility = Visibility.Visible;
                eyecon.Visibility = Visibility.Hidden;

                txtRegPasswordcon.Visibility = Visibility.Hidden;
                txbParolcon.Visibility = Visibility.Visible;
                txbParolcon.Text = txtRegPasswordcon.Password;


            }
            else
            {
                noteyecon.Visibility = Visibility.Hidden;
                eyecon.Visibility = Visibility.Visible;

                txtRegPasswordcon.Visibility = Visibility.Visible;
                txbParolcon.Visibility = Visibility.Hidden;
                txtRegPasswordcon.Password = txbParolcon.Text;
            }
        }
        public bool ContainsNonLatinCharacters(string input)
        {

            string latinAlphaNumericPattern = @"^[a-zA-Z0-9]+$";
            // Check if the input contains any Latin alphabets
            return Regex.IsMatch(input, latinAlphaNumericPattern);
        }
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (ContainsNonLatinCharacters(txtRegPassword.Password) == true && ContainsNonLatinCharacters(txtRegUsername.Text) == true) count++;
            else MessageBox.Show("Faqat lotin alifbosidan foydalaning");

            var checkUsername = await _repository.GetByIdUserName(txtRegUsername.Text);
            if (checkUsername.Count == 0) count++;
            else MessageBox.Show("Bunday foydalanuvchi mavjud");

            if (txtRegUsername.Text.Length >= 8) count++;
            else MessageBox.Show("Ismda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin");

            if (txtRegPassword.Password.Length >= 8) count++;
            else MessageBox.Show("Parolda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin");

            if(txtRegPassword.Password == txtRegPasswordcon.Password) count++;
            else MessageBox.Show("Parol va takroriy parol bir xil bo'lishi kerak");

            if (count == 5)
                {
                var user = new User();
                user.UserName = txtRegUsername.Text;

                var hasherResult = PasswordHasher.Hash(txtRegPassword.Password);
                user.Password = hasherResult.PasswordHash;
                user.Salt = hasherResult.Salt;
                user.Created_at = TimeHelper.GetDateTime();
                user.Updated_at = TimeHelper.GetDateTime();

                var result =await _repository.CreateAsync(user);
                try 
                {
                    if (result > 0)
                    {
                        MessageBox.Show("Muvaffaqiyatli");
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.Password = txtRegPassword.Password;
                        Properties.Settings.Default.Username = txtRegUsername.Text;
                        Properties.Settings.Default.Save();

                        LoginWindow window = new LoginWindow();
                        this.Close();
                        window.Show();
                    }
                    else
                    {
                        MessageBox.Show("Xatoli");
                    }
                }
                catch
                {
                    MessageBox.Show("Databazaga ulanishda xato");
                }
            }
        }

    }
}
