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

namespace Authorization.Desktop.Windows.SubCategories
{
    /// <summary>
    /// Логика взаимодействия для SubCategoryUpdateWindow.xaml
    /// </summary>
    public partial class SubCategoryUpdateWindow : Window
    {
        public SubCategoryUpdateWindow()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
