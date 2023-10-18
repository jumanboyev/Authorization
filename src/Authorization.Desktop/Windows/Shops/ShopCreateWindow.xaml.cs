using Accessibility;
using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Shops;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Authorization.Desktop.Windows.Shops
{
    /// <summary>
    /// Interaction logic for ShopCreateWindow.xaml
    /// </summary>
    public partial class ShopCreateWindow : Window
    {
        private ShopRepository _repository;

        public ShopCreateWindow()
        {
            this._repository = new ShopRepository();
            InitializeComponent();
        }

        private void btnPicture_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;
                ImgShop.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
                ImgIcon.Visibility = Visibility.Hidden;
            }
            ImgIcon.Visibility = Visibility.Hidden;
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtbName.Text.Length > 0 && txtDescription.Text.Length > 0 && ImgShop.ImageSource != null)
            {
                int count = 0;
                Shop shop = new Shop();
                if (txtbName.Text.Length >= 3)
                {
                    shop.Name = txtbName.Text;
                    count++;
                }
                else
                {
                    MessageBox.Show("Do'kon nomi uzunligi kamida 3 ta bo'lishi kerak");
                    return;
                }
                if (txtDescription.Text.Length >= 5)
                {
                    shop.Description = txtDescription.Text;
                    count++;
                }
                else
                {
                    MessageBox.Show("Do'kon tasnifi uzunligi kamida 5 ta bo'lishi kerak");
                    return;
                }
                if (ImgShop.ImageSource != null)
                {
                    shop.Image = ImgShop.ImageSource.ToString();
                    count++;
                }
                else
                {
                    MessageBox.Show("Rasmni tayta tekshiring!");
                    return;
                }
                shop.Created_at = TimeHelper.GetDateTime();
                shop.Updated_at = TimeHelper.GetDateTime();

                if (count == 3)
                {
                    var result = await _repository.CreateAsync(shop);
                    if (result > 0)
                    {
                        MessageBox.Show("Muvaffaqiyatli yaratildi");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xatolik");
                    }
                }
            }
            else
            {
                MessageBox.Show("Maydonlar bo'sh bo'lishi mumkin emas!");
            }
            
        }

        private void txtbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z0-9]"))
            {
                e.Handled = true;
            }            
        }

        private void txtbName_PreviewKeyDown(object sender, KeyEventArgs e)
        {            

            if (e.Key == Key.Space)
            {
               e.Handled = true;
            }            
        }
    }
}
