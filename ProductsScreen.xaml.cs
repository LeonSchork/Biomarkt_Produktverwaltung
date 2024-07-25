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

namespace Biomarkt_App_WPF
{
    /// <summary>
    /// Interaction logic for ProductsScreen.xaml
    /// </summary>
    public partial class ProductsScreen : Window
    {
        public ProductsScreen()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnProductSafe_Click(object sender, RoutedEventArgs e)
        {
            string productName = textBoxProductName.Text;
            Console.WriteLine(productName);
        }

        private void btnProductEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProductClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
