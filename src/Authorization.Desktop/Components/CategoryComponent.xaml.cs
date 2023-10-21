using Authorization.Desktop.Repositories.Categories;
using Authorization.Desktop.ViewModels.Categories;
using Authorization.Desktop.ViewModels.Shops;
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
    /// Interaction logic for CategoryComponent.xaml
    /// </summary>
    public partial class CategoryComponent : Page
    {
        private long shopId {  get; set; }
        private string shopName {  get; set; }= string.Empty;

        private CategoryViewModel viewmodel;
        private CategoryRepository _repository;
        public Func<Task> Refresh {  get; set; }

        public CategoryComponent()
        {
            InitializeComponent();
            this.viewmodel = new CategoryViewModel();
            this._repository = new CategoryRepository();
        }

        public void SetData(long shopId, string shopName)
        {
            this.shopId = shopId;
            this.shopName = shopName;
            lbName.Text = shopName;
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu categoriyani o'chirmoqchimisiz ?", "O'chirish"
                , MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var shopDelete = await _repository.DeleteAsync(viewmodel.Id);
                await Refresh();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
