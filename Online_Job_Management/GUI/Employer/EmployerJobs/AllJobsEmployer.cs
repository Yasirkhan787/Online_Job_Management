using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL.EmployerBLL;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Employer.EmployerJobs
{
    public partial class AllJobsEmployer : Form
    {
        private EmployerBLL employerBLL = new EmployerBLL();
        private int employerId;

        public AllJobsEmployer(int employerID)
        {
            InitializeComponent();
            this.employerId = employerID;
            LoadJobs();
        }

        private void LoadJobs()
        {
            try
            {
                List<Job> jobs = employerBLL.GetAllJobs(employerId);
                dgvJobs.DataSource = jobs;
            
        }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
}



// Handle actions for Edit and Delete buttons in DataGridView
private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            List<Job> jobs = employerBLL.GetAllJobs(employerId);
            dgvJobs.DataSource = jobs;
        }

        // Add Action Button for Edit(Update) and Delete 


        // Application Button
        private void btnApplications_Click(object sender, EventArgs e)
        {
            EmployerDashboard form = new EmployerDashboard(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Jobs Button (Current Page)
        private void btnJobs_Click(object sender, EventArgs e)
        {
            
        }

        // Profile Button
        private void btnProfile_Click(object sender, EventArgs e)
        {
            //
        }

        // Logout Button
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddJobsEmployer form = new AddJobsEmployer(UserSession.UserID);
            form.Show();
            this.Close();
        }

        private void AllJobsEmployer_Load(object sender, EventArgs e)
        {

        }
    }
}
