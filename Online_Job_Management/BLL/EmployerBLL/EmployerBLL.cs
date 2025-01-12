using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Job_Management.DAL.EmployerDAL;
using Online_Job_Management.Model;

namespace Online_Job_Management.BLL.EmployerBLL
{
    internal class EmployerBLL
    {
        private EmployerDAL employerDAL = new EmployerDAL();

        // Get All Applications 
        public List<Application> GetAllApplications(int employerID)
        {
            return employerDAL.GetAllApplications(employerID);
        }

        // Update Application Status
        public bool UpdateApplicationStatus(int applicationID, string status)
        {
            return employerDAL.UpdateApplicationStatus(applicationID, status);
        }

        // Get All Jobs
        public List<Job> GetAllJobs(int employerID)
        {
            return employerDAL.GetAllJobs(employerID);
        }

        // Add New Job
        public bool AddJob(Job job)
        {
            return employerDAL.AddJob(job);
        }

        // Delete Job
        public bool DeleteJob(int jobID)
        {
            return employerDAL.DeleteJob(jobID);
        }

        // Update Job
        public bool UpdateJob(Job job)
        {
            return employerDAL.UpdateJob(job);
        }
    }
}
