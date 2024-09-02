using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
    /// Interaction logic for CreateInvoice.xaml
    /// </summary>
    public partial class CreateInvoice : Window
    {

        public CreateInvoice()
        {
            InitializeComponent();
            ShowInvoice();
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region Helper Methods

        
        private void ShowProducts()
        {
            DataTable productsTable = HelperMethods.GetDataBase("Product");
        }


        private void ShowInvoice()
        {
            DataTable invoiceTable = HelperMethods.GetDataBase("Invoice");
            InvoiceDataGrid.ItemsSource = invoiceTable.DefaultView;
        }


        #endregion
    }
}
