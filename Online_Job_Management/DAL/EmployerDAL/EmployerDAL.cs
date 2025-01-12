using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.Model;

namespace Online_Job_Management.DAL.EmployerDAL
{
    public class EmployerDAL
    {
        // Connection String
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=jobAppDb;Trusted_Connection=True;";


        // Get All Applications 
        public List<Application> GetAllApplications(int employerID)
        {
            List<Application> applications = new List<Application>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT a.ApplicationID, a.JobID, a.JobSeekerID, a.ResumePath, a.Status, a.ApplyDate, 
                           j.Title AS JobTitle, u.Name AS ApplicantName
                    FROM Applications a
                    INNER JOIN Jobs j ON a.JobID = j.JobID
                    INNER JOIN Users u ON a.JobSeekerID = u.UserID
                    WHERE j.EmployerID = @EmployerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployerID", employerID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Application application = new Application
                    {
                        ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                        JobID = Convert.ToInt32(reader["JobID"]),
                        JobSeekerID = Convert.ToInt32(reader["JobSeekerID"]),
                        ResumePath = reader["ResumePath"].ToString(),
                        Status = reader["Status"].ToString(),
                        ApplyDate = Convert.ToDateTime(reader["ApplyDate"]),
                        JobTitle = reader["JobTitle"].ToString(),
                        ApplicantName = reader["ApplicantName"].ToString()
                    };
                    applications.Add(application);
                }
                conn.Close();
            }

            return applications;
        }

    // Update Application Status 
    public bool UpdateApplicationStatus(int applicationID, string status)
     {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Applications SET Status = @Status WHERE ApplicationID = @ApplicationID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@ApplicationID", applicationID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

        // Get All Jobs
        public List<Job> GetAllJobs(int employerID)
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Jobs WHERE EmployerID = @EmployerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployerID", employerID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Job job = new Job
                    {
                        JobID = Convert.ToInt32(reader["JobID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        EmployerID = Convert.ToInt32(reader["EmployerID"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        Location = reader["Location"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        Requirements = reader["Requirements"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        JobType = reader["JobType"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                    jobs.Add(job);
                }
                conn.Close();
            }

            return jobs;
        }

        // Add New Job by Employer
        public bool AddJob(Job job)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Jobs (Title, Description, EmployerID, CategoryID, Location, Salary, Requirements, CompanyName, JobType, Status) 
            VALUES (@Title, @Description, @EmployerID, @CategoryID, @Location, @Salary, @Requirements, @CompanyName, @JobType, @Status)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", job.Title);
                cmd.Parameters.AddWithValue("@Description", job.Description);
                cmd.Parameters.AddWithValue("@EmployerID", job.EmployerID);
                cmd.Parameters.AddWithValue("@CategoryID", job.CategoryID);
                cmd.Parameters.AddWithValue("@Location", job.Location);
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@Requirements", job.Requirements);
                cmd.Parameters.AddWithValue("@CompanyName", job.CompanyName);
                cmd.Parameters.AddWithValue("@JobType", job.JobType);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

        // Delete Job 
        public bool DeleteJob(int jobID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Jobs WHERE JobID = @JobID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@JobID", jobID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

        // Update Job
        public bool UpdateJob(Job job)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            UPDATE Jobs 
            SET Title = @Title, 
                Description = @Description, 
                CategoryID = @CategoryID, 
                Location = @Location, 
                Salary = @Salary, 
                Requirements = @Requirements, 
                CompanyName = @CompanyName, 
                JobType = @JobType,
                Status = @Status
            WHERE JobID = @JobID AND EmployerID = @EmployerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", job.Title);
                cmd.Parameters.AddWithValue("@Description", job.Description);
                cmd.Parameters.AddWithValue("@CategoryID", job.CategoryID);
                cmd.Parameters.AddWithValue("@Location", job.Location);
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@Requirements", job.Requirements);
                cmd.Parameters.AddWithValue("@CompanyName", job.CompanyName);
                cmd.Parameters.AddWithValue("@JobType", job.JobType);
                cmd.Parameters.AddWithValue("@JobID", job.JobID);
                cmd.Parameters.AddWithValue("@EmployerID", job.EmployerID);
                cmd.Parameters.AddWithValue("@Status", job.Status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }

    }
}

