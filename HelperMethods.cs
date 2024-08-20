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

namespace Biomarkt_App_WPF
{
    public static class HelperMethods
    {
        public static DataTable ExecuteQuery(SqlConnection dbConnection, string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
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
                dbConnection.Close();
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
            if(!float.TryParse(input, out _))
            {
                MessageBox.Show("Bitte gib eine Gültige Zahl ein");
                return false;
            }
            return true;
        }
    }
}
