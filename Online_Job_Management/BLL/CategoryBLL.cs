using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.DAL;
using Online_Job_Management.Model;

namespace Online_Job_Management.BLL
{
    public class CategoryBLL
    {
        private CategoryDAL categoryDAL;

        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();  // Instantiate the DAL class
        }

        // Method to get all categories from the database
        public List<Category> GetAllCategories()
        {
            try
            {
                return categoryDAL.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CategoryBLL.GetAllCategories: " + ex.Message);
            }
        }

        // Method to get a single category by ID (optional, if needed)
        public Category GetCategoryById(int categoryId)
        {
            try
            {
                return categoryDAL.GetCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CategoryBLL.GetCategoryById: " + ex.Message);
            }
        }

        // Method to add a new category to the database
        public bool AddCategory(Category category)
        {
            try
            {
                return categoryDAL.AddCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CategoryBLL.AddCategory: " + ex.Message);
            }
        }

        // Method to update an existing category in the database
        public bool UpdateCategory(Category category)
        {
            try
            {
                return categoryDAL.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CategoryBLL.UpdateCategory: " + ex.Message);
            }
        }

        // Method to delete a category from the database
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                return categoryDAL.DeleteCategory(categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CategoryBLL.DeleteCategory: " + ex.Message);
            }
        }
    }
}
