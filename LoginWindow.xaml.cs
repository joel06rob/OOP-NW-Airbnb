using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_NW_Airbnb
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Database db = new Database();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text;
            string password = PasswordBox.Password;


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Condition to check login details with admin details in DB
            if (ValidateLogin(username, password))
            {
                MessageBox.Show("Login Success");
            }
            else
            {
                MessageBox.Show("Login Failed, Try Again!");
            }


        }

        private bool ValidateLogin(string username, string password)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(1) FROM Admin WHERE Username = @Username AND Password = @Password";
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count == 1;
            }
        }

    }
}
