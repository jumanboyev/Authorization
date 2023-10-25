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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Authorization.Desktop.Components
{
    /// <summary>
    /// Логика взаимодействия для ProductComponent.xaml
    /// </summary>
    public partial class ProductComponent : UserControl
    {
        public Func<Task> Refresh { get; set; }

        private ProductViewModel viewModel;
        private ProductRepository _repository;

        public ProductComponent()
        {
            InitializeComponent();
            this.viewModel = new ProductViewModel();
            this._repository = new ProductRepository();
        }

        public void SetData(ProductViewModel productViewModel)
        {
            viewModel = productViewModel;
            lbName.Text = productViewModel.Name;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}