﻿using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Pages;
using Authorization.Desktop.Pages.Cashboxes;
using Authorization.Desktop.Pages.Categories;
using Authorization.Desktop.Pages.Products;
using Authorization.Desktop.Pages.SubCategories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Authorization.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void rbShop_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shop = new ShopPage();
            PageNavigator.Content = shop;

        }

        private void Window_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void btnBackto_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shop = new ShopPage();
            PageNavigator.Content = shop;
            btnBackto.Visibility = Visibility.Collapsed;    
            rbProduct.Visibility = Visibility.Collapsed;
            rbStorage.Visibility = Visibility.Collapsed;
            rbKassa.Visibility = Visibility.Collapsed;
            rbShop.Visibility = Visibility.Visible;
        }


        private void rbSubCategory_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.Navigate(new Uri("Pages/Subcategories/SubCategoryPage.xaml", UriKind.Relative));
        }

        private void rbCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryPage category = new CategoryPage();
            PageNavigator.Content = category;
        }

        private void rbProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductPage productPage = new ProductPage();
            PageNavigator.Content = productPage;    
        }

        private void rbStorage_Click(object sender, RoutedEventArgs e)
        {
            StoragePage storagePage = new StoragePage();
            PageNavigator.Content = storagePage;
        }

        private void rbKassa_Click(object sender, RoutedEventArgs e)
        {
            CashboxPage kassa = new CashboxPage();
            PageNavigator.Content = kassa;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
