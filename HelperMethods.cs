using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Common;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;

namespace Biomarkt_App_WPF
{
    public static class HelperMethods
    {
        public static SqlConnection GetDbConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ProNaturDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = GetDbConnection();
            try
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataTable;
        }

        public static bool ValidateStringInput(string[] inputs)
        {
            foreach (var input in inputs)
            {
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Bitte fülle alle Werte aus");
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateFloatInput(string input)
        {
            if (!float.TryParse(input, out _))
            {
                MessageBox.Show("Bitte gib eine Gültige Zahl ein");
                return false;
            }
            return true;
        }


        public static DataTable GetDataBase(string tableName)
        {
            // Validate and sanitize the table name
            if (string.IsNullOrEmpty(tableName) || !IsValidTableName(tableName))
            {
                throw new ArgumentException($"Invalid table name. {tableName}");
            }

            string query = $"SELECT * FROM {tableName}";
            return ExecuteQuery(query, null);
        }

        private static bool IsValidTableName(string tableName)
        {
            string[] allowedTables = { "Product", "Invoice", "InvoiceItem", "Customer"};

            if (allowedTables.Contains(tableName) && tableName.All(char.IsLetterOrDigit))
            {
                return true;

            }
            else return false;
        }

    }
}
