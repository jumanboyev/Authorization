using Authorization.Desktop.ViewModels.SaleProducts;
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
    /// Логика взаимодействия для SaleProductComponent.xaml
    /// </summary>
    public partial class SaleProductComponent : UserControl
    {
        private SaleProductViewModel viewModel;

        public SaleProductComponent()
        {
            InitializeComponent();
            this.viewModel = new SaleProductViewModel();
        }

        public void SetData(SaleProductViewModel saleProductViewModel)
        {
            this.viewModel = saleProductViewModel;
            lbName.Text = saleProductViewModel.Name;
        }
        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
