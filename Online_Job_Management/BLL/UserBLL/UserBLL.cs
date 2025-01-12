using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.DAL.UserDAL;

namespace Online_Job_Management.BLL.UserBLL
{
    public class UserBLL
    {
        // Creating Object of UserDAL
        private UserDAL userDAL = new UserDAL();

        // Register User 
        public bool Register(string username, string name, string email, string password, string role, string phone)
        {
            // Check if username is already taken
            if (userDAL.IsUsernameTaken(username))
            {
                throw new Exception("Username is already taken.");
            }

            // Register the user
            return userDAL.RegisterUser(username, name, email, password, role, phone);
        }

        // Login User
        public DataRow Login(string email, string password)
        {
            // Add additional validation logic if required
            return userDAL.AuthenticateUser(email, password);
        }
    }
}
