using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Data.SqlClient;

namespace OOP_NW_Airbnb
{
    //TODO: Change DB Connection to SQL Server (College Server)
    public class Database
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ChatBotDBConnectionString"].ConnectionString;
    
        //Public method to return a new SqlConnection - So other methods in classes can dynamically select this.
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString); 
        }
    
    }
}
