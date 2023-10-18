using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Interfaces.Users;
using Authorization.Desktop.Repositories.Users;
using Authorization.Desktop.Security;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;

namespace Authorization.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly IUserRepository _repository;
        public string UserPassword { get; set; }
        private string UserPasswordCon { get; set; }

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
        public bool isDigit_UpperCase(string input)
        {
            bool hasUppercase = false;
            bool hasDigit = false;

            foreach (char i in input)
            {
                if (char.IsUpper(i))
                    hasUppercase = true;

                if (char.IsDigit(i))
                    hasDigit = true;

                if (hasUppercase && hasDigit)
                    return true;
            }

            return false;
        }
        
        private void txtRegPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z0-9]"))
            {
                e.Handled = true;
            }
        }
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtRegPassword.Visibility == Visibility.Visible ) UserPassword = txtRegPassword.Password;
            else UserPassword = txbParol.Text;

            if (txtRegPasswordcon.Visibility == Visibility.Visible) UserPasswordCon = txtRegPasswordcon.Password;
            else UserPasswordCon = txbParolcon.Text;

            if (txtRegUsername.Text.Length > 0 && UserPassword.Length > 0 && UserPasswordCon.Length > 0)
            {
                int count = 0;
                if (ContainsNonLatinCharacters(txtRegUsername.Text) == true && ContainsNonLatinCharacters(UserPassword) == true && ContainsNonLatinCharacters(UserPasswordCon) == true) count++;
                else { MessageBox.Show("Faqat lotin alifbosidan foydalaning"); 
                    return;
                }

                var checkUsername = await _repository.GetByIdUserName(txtRegUsername.Text);
                if (checkUsername.Count == 0) count++;
                else { MessageBox.Show("Bunday foydalanuvchi mavjud");
                    return;
                }

                if (txtRegUsername.Text.Length >= 8) count++;
                else { MessageBox.Show("Ismda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin"); return; }

                if (isDigit_UpperCase(UserPassword) == true) count++;
                else { MessageBox.Show("Parolda kamida 1 ta raqam va 1 ta katta harf bo'lishi kerak "); return; }

                if (UserPassword.Length >= 8) count++;
                else{ MessageBox.Show("Parolda faqat harf,raqam qatnashsin va uzunligi kamida 8 ta bo'lsin"); return; }

                if (UserPassword == UserPasswordCon) count++;
                else { MessageBox.Show("Parol va takroriy parol bir xil bo'lishi kerak"); return; }

                if (count == 6)
                {
                    var user = new User();
                    user.UserName = txtRegUsername.Text;

                    var hasherResult = PasswordHasher.Hash(UserPassword);
                    user.Password = hasherResult.PasswordHash;
                    user.Salt = hasherResult.Salt;
                    user.Created_at = TimeHelper.GetDateTime();
                    user.Updated_at = TimeHelper.GetDateTime();

                    var result = await _repository.CreateAsync(user);
                    try
                    {
                        if (result > 0)
                        {
                            MessageBox.Show("Muvaffaqiyatli");
                            Properties.Settings.Default.RememberMe = true;
                            Properties.Settings.Default.Password = UserPassword;
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
            else
            {
                MessageBox.Show("Maydonlar bo'sh bo'lishi mumkin emas!");
            }
        }

        private void txbParol_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

    }
}
