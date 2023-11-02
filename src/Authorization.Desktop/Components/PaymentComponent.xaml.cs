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
    /// Логика взаимодействия для PaymentComponent.xaml
    /// </summary>
    public partial class PaymentComponent : UserControl
    {
        public PaymentComponent()
        {
            InitializeComponent();
        }

        public string FPrice(string Price)
        {
            float number = float.Parse(Price);
            string formattedNumber = number.ToString("#,##0").Replace(",", " ");
            return formattedNumber;
        }
        public void SetData(SaleProductViewModel saleProductViewModel)
        {
            lbProductName.Content = saleProductViewModel.Name;
            string FormattedSoldPrice = FPrice(saleProductViewModel.SoldPrice.ToString());
            lbProductPrice.Content = FormattedSoldPrice;
            string FormattedQuantity = FPrice((saleProductViewModel.productquantity).ToString());
            lbQuantity.Content = FormattedQuantity;
            float TotalSum = saleProductViewModel.SoldPrice * saleProductViewModel.productquantity;
            string FormattedTotalPrice = FPrice((TotalSum).ToString());
            lbTotalPrice.Content = FormattedTotalPrice;
        }
    }
}
