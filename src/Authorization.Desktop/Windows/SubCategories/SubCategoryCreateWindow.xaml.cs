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
    /// Логика взаимодействия для SubCategoryCreateWindow.xaml
    /// </summary>
    public partial class SubCategoryCreateWindow : Window
    {
        public SubCategoryCreateWindow()
        {
            InitializeComponent();
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
