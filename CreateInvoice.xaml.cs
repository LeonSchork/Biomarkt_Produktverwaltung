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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Biomarkt_App_WPF
{
    /// <summary>
    /// Interaction logic for CreateInvoice.xaml
    /// </summary>
    public partial class CreateInvoice : Window
    {

        // Fields
        private int lastSelectedInvoiceKey;

        // Constructor
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
            // Check if there is a selected item
            if (InvoiceDataGrid.SelectedItem is DataRowView rowView)
            {
                lastSelectedInvoiceKey = (int)rowView["InvoiceId"];
            }

        }

        private void ShowInvoiceItems_Click(object sender, RoutedEventArgs e)
        {
            InvoiceItemSum.Content = ShowInvoiceDetails(lastSelectedInvoiceKey);
        }


        #region Helper Methods
        private decimal ShowInvoiceDetails(int id)
        {
            string query = @"SELECT 
                                Invoice.InvoiceId, 
                                item.InvoiceId, 
                                item.ItemQuantity, 
                                item.ItemPrice, 
                                Product.ProductName 
                            FROM 
                                [dbo].[InvoiceItem] item
                            INNER JOIN 
                                Invoice ON Invoice.InvoiceId = item.InvoiceId 
                            INNER JOIN 
                                Product ON Product.Id = item.ProductId 
                            WHERE 
                                Invoice.InvoiceId = @InvoiceId";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@InvoiceId", id)
            };
            DataTable detailsTable = HelperMethods.ExecuteQuery(query, parameter);
            InvoiceItemsDG.ItemsSource = detailsTable.DefaultView;

            decimal overallPrice = 0;
            foreach (DataRow row in detailsTable.Rows)
            {
                int itemQuantity = Convert.ToInt32(row["ItemQuantity"]);
                decimal itemPrice = Convert.ToDecimal(row["ItemPrice"]);
                overallPrice += itemPrice * itemQuantity;
            }
            return overallPrice;
        }


        private void ShowInvoice()
        {
            string query = @"
                SELECT 
                    i.InvoiceId,
                    i.CustomerId,
                    c.CustomerName,
                    c.CustomerAddress,
                    i.InvoiceDate
                FROM 
                    [dbo].[Invoice] i
                INNER JOIN 
                    [dbo].[Customer] c ON i.CustomerId = c.CustomerId;
            ";
            DataTable invoiceTable = HelperMethods.ExecuteQuery(query,null);
            InvoiceDataGrid.ItemsSource = invoiceTable.DefaultView;
        }


        #endregion

    }
}
