using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using crud.Models;


namespace crud.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["qty"]),
                        Remarks = dr["Remarks"].ToString(),
                        Category = dr["Category"] != DBNull.Value ? dr["Category"].ToString() : "", 
                        ProductType = dr["ProductType"] != DBNull.Value ? dr["ProductType"].ToString() : "", 
                        ManufactureDate = dr["ManufactureDate"] != DBNull.Value ? Convert.ToDateTime(dr["ManufactureDate"]) : DateTime.MinValue, 
                        InStock = dr["InStock"] != DBNull.Value ? Convert.ToBoolean(dr["InStock"]) : false 

                    });
                }
            }
            return productList;
        }

        //Insert product data
        public int InsertProducts(Product product)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@ProductType", product.ProductType);
                command.Parameters.AddWithValue("@ManufactureDate", product.ManufactureDate);
                command.Parameters.AddWithValue("@InStock", product.InStock);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }


        //Get Products by ProductId
        public Product GetProductByID(int ProductID)
        {
            Product product = null;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductByID";
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                if (dtProducts.Rows.Count > 0)
                {
                    DataRow dr = dtProducts.Rows[0];
                    product = new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = dr.IsNull("Price") ? 0 : Convert.ToDecimal(dr["Price"]),
                        Qty = dr.IsNull("Qty") ? 0 : Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString(),
                        Category = dr["Category"].ToString(),
                        ProductType = dr["ProductType"].ToString(),
                        ManufactureDate = dr.IsNull("ManufactureDate") ? (DateTime?)null : Convert.ToDateTime(dr["ManufactureDate"]),
                        InStock = dr.IsNull("InStock") ? false : Convert.ToBoolean(dr["InStock"])
                    };
                }
            }
            return product;
        }



        //Update product data
        public int UpdateProducts(Product product)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@ProductType", product.ProductType);
                command.Parameters.AddWithValue("@ManufactureDate", product.ManufactureDate);
                command.Parameters.AddWithValue("@InStock", product.InStock);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }


        //Delete product data
        public int DeleteProducts(int ProductID)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", ProductID);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
    }
}