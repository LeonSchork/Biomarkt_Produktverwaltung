using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ProductsScreen.xaml
    /// </summary>
    public partial class ProductsScreen : Window
    {
        // Fields
        private SqlConnection dbConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pavel\Documents\ProNaturDB.mdf;Integrated Security=True;Connect Timeout=30");
        private int lastSelectedProductKey;

        // Constructor
        public ProductsScreen()
        {
            InitializeComponent();
            ShowProducts();
        }

        // Event handler to save a new product to the database
        private void btnProductSafe_Click(object sender, RoutedEventArgs e)
        {
            string[] inputValues = { textBoxProductName.Text, textBoxProductBrand.Text, comboBoxProductCategory.Text };

            if (HelperMethods.ValidateStringInput(inputValues) && HelperMethods.ValidateFloatInput(textBoxProductPrice.Text))
            {
                Product product = GetInputFields();
                string query = "INSERT INTO Products (ProductName, ProductBrand, ProductCategory, ProductPrice) VALUES (@ProductName, @ProductBrand, @ProductCategory, @ProductPrice)";
                SqlParameter[] parameters = {
                    new SqlParameter("@ProductName", product.Name),
                    new SqlParameter("@ProductBrand", product.Brand),
                    new SqlParameter("@ProductCategory", product.Category),
                    new SqlParameter("@ProductPrice", product.Price) 
                };
                HelperMethods.ExecuteQuery(dbConnection, query, parameters);
                ShowProducts();
                ClearProductFields();
            }

        }

        // Event handler to edit the selected product
        private void btnProductEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Product aus");
                return;
            }
            Product product = GetInputFields();
            string query = "UPDATE Products SET ProductName = @ProductName, ProductBrand = @ProductBrand, ProductCategory = @ProductCategory, ProductPrice = @ProductPrice WHERE Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id", lastSelectedProductKey),
                new SqlParameter("@ProductName", product.Name),
                new SqlParameter("@ProductBrand", product.Brand),
                new SqlParameter("@ProductCategory", product.Category),
                new SqlParameter("@ProductPrice", product.Price)
            };
            HelperMethods.ExecuteQuery(dbConnection, query, parameters);
            ShowProducts();
        }

        // Event handler to delete the selected product
        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete selected product
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("bitte wähle ein Element aus");
                return;
            }

            string query = "DELETE FROM Products WHERE Id = @Id";
            SqlParameter[] parameters = {new SqlParameter("@Id", lastSelectedProductKey)};
            HelperMethods.ExecuteQuery(dbConnection, query, parameters);
            ShowProducts();
        }

        // Event handler to clear the product input fields
        private void btnProductClear_Click(object sender, RoutedEventArgs e)
        {
            ClearProductFields();
        }

        // Event handler for item selection by user
        private void ProductsDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if there is a selected item
            if (ProductsDGV.SelectedItem is DataRowView rowView)
            {
                // Extract the value from the selected item
                string productName = rowView["ProductName"].ToString();
                string productBrand = rowView["ProductBrand"].ToString();
                string productCategory = rowView["ProductCategory"].ToString();
                string productPrice = rowView["ProductPrice"].ToString();

                // Populate the input fields with the extracted values
                textBoxProductName.Text = productName;
                textBoxProductBrand.Text = productBrand;
                comboBoxProductCategory.Text = productCategory;
                textBoxProductPrice.Text = productPrice;

                lastSelectedProductKey = (int)rowView["Id"];
            }
        }
        //Event handler to close products screen and reopen main menu
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        #region Dev stuff
        // Event handler to insert test data into the input fields
        private void InsertTestData_Click(object sender, RoutedEventArgs e)
        {
            GenerateTestData();
        }


        // Event handler to delete all products with the category 'TestData' from the database
        private void DeleteTestData_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Möchtest du wirklich alle TestData Einträge Löschen?", "Bestätigen", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                string query = "DELETE FROM Products WHERE ProductCategory = @ProductCategory";
                SqlParameter[] parameter = { new SqlParameter("@ProductCategory", "TestData") };

                HelperMethods.ExecuteQuery(dbConnection, query, parameter);
            }
            ShowProducts();
        }

        private void GenerateTestData()
        {
            textBoxProductName.Text = "TestData";
            textBoxProductBrand.Text = "TestData";
            textBoxProductPrice.Text = "111";
            comboBoxProductCategory.SelectedIndex = 0;
        }
        #endregion


        #region Helper Methods

        private void ShowProducts()
        {
            string query = "select * from Products";
            DataTable productsTable = HelperMethods.ExecuteQuery(dbConnection, query, null);
            ProductsDGV.ItemsSource = productsTable.DefaultView;
        }

        private Product GetInputFields()
        {
            
            Product product = new Product
                (
                textBoxProductName.Text, 
                textBoxProductBrand.Text, 
                comboBoxProductCategory.Text, 
                float.Parse(textBoxProductPrice.Text)
                );

            return product;
        }

        private void ClearProductFields()
        {
            textBoxProductName.Text = "";
            textBoxProductBrand.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.SelectedIndex = -1;
        }
        #endregion

    }
}
