using Authorization.Desktop.ViewModels.Tabs;
using Authorization.Desktop.Windows.CashboxProducts;
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
    /// Логика взаимодействия для TabComponent.xaml
    /// </summary>
    public partial class TabComponent : UserControl
    {
        public long TabId {  get; set; }

        public delegate void UserControlClickedEventHandler(long id);
        public event UserControlClickedEventHandler UserControlClicked;

        CashboxProductWindow ParentWindow;
        public TabComponent(CashboxProductWindow paymentWindow)
        {
            InitializeComponent();
            this.ParentWindow = paymentWindow;
        }

        public void SetData(TabViewModel tabViewModel)
        {
            this.TabId = tabViewModel.Id;
            lbName.Text = tabViewModel.Name;            
        }

        private void UserControl_Click(object sender, RoutedEventArgs e)
        {

        }   

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControlClicked?.Invoke(TabId);

            ParentWindow.ClearUserControlBorder();

            B.BorderBrush = Brushes.Red;
        }
    }
}
