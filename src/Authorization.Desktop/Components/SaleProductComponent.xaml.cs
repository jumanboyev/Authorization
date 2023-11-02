using Authorization.Desktop.Security;
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
        public delegate void RefreshPaymendWindowThirdWrapPanel();
        public RefreshPaymendWindowThirdWrapPanel? RefreshThirdWrapPanel { get; set; }
        public SaleProductViewModel? saleProductViewModel;
        public long CashboxId { get; set; }

        public SaleProductComponent()
        {
            InitializeComponent();
        }

        public void SetData(SaleProductViewModel saleProductViewModel, long cashboxId)
        {
            this.CashboxId = cashboxId;
            saleProductViewModel.ShopId = cashboxId;
            this.saleProductViewModel = saleProductViewModel;
            lbName.Text = saleProductViewModel.Name;
        }
        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var sellProductList = identity.AddToCartList;
            int checkName = 0;
            int countIteration = 0;
            bool isOldProduct = false;

            for (int i = 0; i < sellProductList.Count; i++)
            {
                countIteration++;
                if (sellProductList[i].Name == saleProductViewModel?.Name && sellProductList[i].ShopId == CashboxId)
                {
                    checkName = i;
                    isOldProduct = true;
                    break;
                }
            }
            if (isOldProduct == true && saleProductViewModel?.SoldPrice != null)
            {
                if (sellProductList[checkName].Quantity > 0) /*> sellProductList[checkName].quantity)*/
                {
                    sellProductList[checkName].productquantity++;
                    sellProductList[checkName].Quantity--;
                    RefreshThirdWrapPanel?.Invoke();
                }
                else MessageBox.Show("Maxsulot qolmadi !");
            }
            else
            {
                if (isOldProduct == false && saleProductViewModel != null)
                {
                    if (saleProductViewModel.Quantity > 0)
                    {
                        sellProductList.Add(saleProductViewModel);
                        sellProductList[countIteration].productquantity++;
                        sellProductList[countIteration].Quantity--;
                        RefreshThirdWrapPanel?.Invoke();
                    }
                    else MessageBox.Show("Maxsulot qolmadi !");

                }
            }
        }
    }
}
