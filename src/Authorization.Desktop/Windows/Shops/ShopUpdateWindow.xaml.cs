    using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Helpers;
using Authorization.Desktop.Repositories.Shops;
using Authorization.Desktop.ViewModels.Shops;
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
    /// Interaction logic for ShopUpdateWindow.xaml
    /// </summary>
    public partial class ShopUpdateWindow : Window
    {
        private ShopRepository _repository;
        public long Id {get;set;}

        public ShopUpdateWindow()
        {
            this._repository = new ShopRepository();
            InitializeComponent();
        }

        public void SetData(Shop shop)
        {
            this.Id = shop.Id;
            txtbName.Text = shop.Name;
            txtDescription.Text = shop.Description;
            ImgShop.ImageSource = new BitmapImage(new System.Uri(shop.Image, UriKind.Relative));
        }
        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPicture_MouseDown(object sender, MouseButtonEventArgs e)
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

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
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
            if (txtDescription.Text.Length >=8)
            {
                shop.Description = txtDescription.Text;
                count++;
            }
            else
            {
                MessageBox.Show("Do'kon tasnifi uzunligi kamida 8 ta bo'lishi kerak");
                return;
            }
            if (shop.Image != null)
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
                var result = await _repository.UpdateAsync(Id,shop);
                if (result > 0)
                {
                    MessageBox.Show("Muvaffaqiyatli tahrirlandi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xatolik");
                }
            }
            
        }

        private void txtbName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z0-9]"))
            {
                e.Handled = true;
            }
        }
    }
}

