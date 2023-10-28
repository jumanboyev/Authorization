﻿using Authorization.Desktop.Repositories.Products;
using Authorization.Desktop.ViewModels.Products;
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

namespace Authorization.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for StorageUpdateWindow.xaml
    /// </summary>
    public partial class StorageUpdateWindow : Window
    {
        private ProductRepository _repository;
        private ProductAllToStorageViewModel viewModel;

        public StorageUpdateWindow()
        {
            InitializeComponent();
            this._repository = new ProductRepository();
            this.viewModel = new ProductAllToStorageViewModel();
        }
        public void SetData()
        {

        }

        private void txtSoldPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtBarCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtBarCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
