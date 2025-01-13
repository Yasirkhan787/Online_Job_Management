using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.DAL.AdminDAL;
using Online_Job_Management.Model;

namespace Online_Job_Management.BLL.AdminBLL
{
    public class AdminBLL
    {
        private AdminDAL adminDAL = new AdminDAL();


        // Get All Users From Database 
        public DataTable GetAllUsers()
        {
            return adminDAL.FetchAllUsers();
        }
        // Add New User
        public bool AddUser(string username, string name, string email, string password, string role, string phone)
        {

            return adminDAL.AddUser(username, name, email, password, role, phone);
        }
        // Update User 
        public bool UpdateUser(int userId, string username, string email, string name, string role, string phone)
        {
            return adminDAL.UpdateUser(userId, username, name, email, role, phone);
        }

        // Delete User
        public bool DeleteUser(int userId)
        {
            return adminDAL.DeleteUser(userId);
        }
        // 
        public DataTable GetPendingJobs()
        {
            return adminDAL.FetchPendingJobs();
        }

        //
        public void ApproveJob(int jobId)
        {
            adminDAL.UpdateJobStatus(jobId, "Approved");
        }

        //
        public void RejectJob(int jobId)
        {
            adminDAL.UpdateJobStatus(jobId, "Rejected");
        }

        //
        public DataTable GetActivityLogs()
        {
            return adminDAL.FetchActivityLogs();
        }

        public List<Job> GetAllJobs() => adminDAL.GetAllJobs();

        // JobSeeker 
        // Approved Jobs
        public List<Job> GetAllApprovedJobs() => adminDAL.FetchApprovedJobs();
        //Apply Job
        public bool ApplyForJob(Application application)
        {
            try
            {
                // Pass the Application object to the DAL layer
                return adminDAL.ApplyForJob(application);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BLL while applying for job: " + ex.Message);
            }
        }
        // Get User By ID
        public User GetUserByID(int userID)
        {
            try
            {
                return adminDAL.FetchUserByID(userID);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch user details.", ex);
            }
        }
    }

}


