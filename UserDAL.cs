using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using crud.Models;

namespace crud.DAL
{
    public class User_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        public bool RegisterUser(string username, string password, string email, string gender, string country)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Email, Gender, Country) VALUES (@Username, @Password, @Email, @Gender, @Country)", con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Country", country);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                return rows > 0;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_AuthenticateUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int result = (int)command.ExecuteScalar();
                isAuthenticated = result > 0;
                connection.Close();
            }

            return isAuthenticated;
        }

    }
}
 