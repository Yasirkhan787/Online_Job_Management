using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.Model;

namespace Online_Job_Management.DAL
{
    public class CategoryDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        // Method to fetch all categories of Employer
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CategoryID, Name FROM Categories";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            Name = reader["Name"].ToString()
                        };
                        categories.Add(category);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CategoryDAL.GetAllCategories: " + ex.Message);
                }
            }

            return categories;
        }

        // Method to fetch a category by ID from the database
        public Category GetCategoryById(int categoryId)
        {
            Category category = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CategoryID, Name FROM Categories WHERE CategoryID = @CategoryID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            Name = reader["Name"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CategoryDAL.GetCategoryById: " + ex.Message);
                }
            }

            return category;
        }

        // Method to add a new category to the database
        public bool AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Categories (Name) VALUES (@Name)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", category.Name);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CategoryDAL.AddCategory: " + ex.Message);
                }
            }
        }

        // Method to update an existing category in the database
        public bool UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Categories SET Name = @Name WHERE CategoryID = @CategoryID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CategoryDAL.UpdateCategory: " + ex.Message);
                }
            }
        }

        // Method to delete a category from the database
        public bool DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CategoryDAL.DeleteCategory: " + ex.Message);
                }
            }
        }
    }
}
