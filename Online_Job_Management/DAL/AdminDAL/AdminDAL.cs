using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.Model;

namespace Online_Job_Management.DAL.AdminDAL
{
    public class AdminDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        // Fetch All Users
        public DataTable FetchAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable usersTable = new DataTable();
                adapter.Fill(usersTable);
                return usersTable;
            }
        }

        // Add New User 
        public bool AddUser(string username, string name, string email, string password, string role, string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Users (Username, Name, Email, Password, Role, Phone)
            VALUES (@Username, @Name, @Email, @Password, @Role, @Phone)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Phone", phone);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        // Update User
        public bool UpdateUser(int userId, string username, string name, string email, string role, string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Users 
                SET Username = @Username, Name = @Name, Email=@Email, Role = @Role, Phone = @Phone 
                WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Delete User
        public bool DeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Fetch Pending Jobs
        public DataTable FetchPendingJobs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs WHERE Status = 'Pending'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable jobsTable = new DataTable();
                adapter.Fill(jobsTable);
                return jobsTable;
            }
        }

        // To Approve and Reject Jobs
        public void UpdateJobStatus(int jobId, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Jobs SET Status = @Status WHERE JobID = @JobID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@JobID", jobId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Admin Log Activity
        public void LogActivity(int adminId, string action)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ActivityLogs (AdminID, Action) VALUES (@AdminID, @Action)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                cmd.Parameters.AddWithValue("@Action", action);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Fetch Activity Logs
        public DataTable FetchActivityLogs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ActivityLogs ORDER BY Timestamp DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable logsTable = new DataTable();
                adapter.Fill(logsTable);
                return logsTable;
            }
        }

        public List<Job> GetAllJobs()
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs.Add(new Job
                    {
                        JobID = Convert.ToInt32(reader["JobID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        EmployerID = Convert.ToInt32(reader["EmployerID"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        Location = reader["Location"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        Requirements = reader["Requirements"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return jobs;
        }

        public List<Job> GetPendingJobs()
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs WHERE Status = 'Pending'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs.Add(new Job
                    {
                        JobID = Convert.ToInt32(reader["JobID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        EmployerID = Convert.ToInt32(reader["EmployerID"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        Location = reader["Location"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        Requirements = reader["Requirements"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return jobs;
        }

        public bool AddJob(Job job)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Jobs (Title, Description, EmployerID, CategoryID, Location, Salary, Requirements, PostDate, ExpiryDate, Status) " +
                               "VALUES (@Title, @Description, @EmployerID, @CategoryID, @Location, @Salary, @Requirements, @PostDate, @ExpiryDate, @Status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", job.Title);
                cmd.Parameters.AddWithValue("@Description", job.Description);
                cmd.Parameters.AddWithValue("@EmployerID", job.EmployerID);
                cmd.Parameters.AddWithValue("@CategoryID", job.CategoryID);
                cmd.Parameters.AddWithValue("@Location", job.Location);
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@Requirements", job.Requirements);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateJob(Job job)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Jobs SET Title = @Title, Description = @Description, EmployerID = @EmployerID, CategoryID = @CategoryID, " +
                               "Location = @Location, Salary = @Salary, Requirements = @Requirements, ExpiryDate = @ExpiryDate, Status = @Status " +
                               "WHERE JobID = @JobID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@JobID", job.JobID);
                cmd.Parameters.AddWithValue("@Title", job.Title);
                cmd.Parameters.AddWithValue("@Description", job.Description);
                cmd.Parameters.AddWithValue("@EmployerID", job.EmployerID);
                cmd.Parameters.AddWithValue("@CategoryID", job.CategoryID);
                cmd.Parameters.AddWithValue("@Location", job.Location);
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@Requirements", job.Requirements);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteJob(int jobId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Jobs WHERE JobID = @JobID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@JobID", jobId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // JobSeeker Methods 
        // Fetch Approved Jobs 
        public List<Job> FetchApprovedJobs()
        {
            List<Job> jobs = new List<Job>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs WHERE Status = 'Approved'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs.Add(new Job
                    {
                        JobID = Convert.ToInt32(reader["JobID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        EmployerID = Convert.ToInt32(reader["EmployerID"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        Location = reader["Location"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        Requirements = reader["Requirements"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return jobs;
        }
        
        // ADD Applications 
        public bool ApplyForJob(Application application)
        {
            string query = @"INSERT INTO JobApplications 
                        (JobID, JobSeekerID, ResumePath, Status, ApplyDate) 
                        VALUES 
                        (@JobID, @JobSeekerID, @ResumePath, @Status, @ApplyDate)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@JobSeekerID", application.JobSeekerID);
                        command.Parameters.AddWithValue("@ResumePath", application.ResumePath);
                        command.Parameters.AddWithValue("@Status", "Pending"); // Default status is Pending
                        command.Parameters.AddWithValue("@ApplyDate", DateTime.Now);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0; // Return true if the insert operation was successful
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DAL while applying for job: " + ex.Message);
            }
        }
        // Get User By Id 
        public User FetchUserByID(int userID)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, UserName, Email, Name, Phone, Role " +
                               "FROM Users WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Name = reader["Name"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
                reader.Close();
            }
            return user;
        }
    }
}


