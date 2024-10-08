﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biomarkt_App_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            
        }

        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsScreen productsScreen = new ProductsScreen();
            productsScreen.Show();
            this.Close();
        }

        private void invoiceButton_Click(object sender, RoutedEventArgs e)
        {
            CreateInvoice createInvoice = new CreateInvoice();
            createInvoice.Show();
            this.Close();
        }
    }

}
