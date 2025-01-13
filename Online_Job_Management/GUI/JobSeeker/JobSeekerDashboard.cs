using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL.AdminBLL;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.JobSeeker
{
    public partial class JobSeekerDashboard : Form
    {
        private AdminBLL adminBLL = new AdminBLL();

        public JobSeekerDashboard()
        {
            InitializeComponent();
            LoadApprovedJobs();
        }
        // LoadPending Project 
        private void LoadApprovedJobs()
        {
            try
            {
                // Fetch data from BLL which calls DAL to get data from the DB
                List<Job> jobs = adminBLL.GetAllApprovedJobs();

                // Bind the data to the DataGridView
                dgvJobs.DataSource = jobs;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Add action buttons to DataGridView 
        //private void AddActionButtons()
        //{
        //    //
        //    if (!dgvJobs.Columns.Contains("Apply"))
        //    {
        //        DataGridViewButtonColumn approveButtonColumn = new DataGridViewButtonColumn();
        //        approveButtonColumn.Name = "Approve";
        //        approveButtonColumn.Text = "Approve";
        //        approveButtonColumn.UseColumnTextForButtonValue = true;
        //        dgvJobs.Columns.Add(approveButtonColumn);
        //    }
        //}
        private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Check if the clicked cell is a button cell
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    string columnName = dgvJobs.Columns[e.ColumnIndex].HeaderText;

            //    if (columnName == "Apply")
            //    {
            //        // Handle Apply Job logic for job
            //        int jobID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value);

            //        // Add More Details 
            //        ApplyJobForm form = new ApplyJobForm(UserSession.UserID);
            //        form.Show();
            //        this.Close();
            //    }
            //}
        }

        private void JobSeekerDashboard_Load(object sender, EventArgs e)
        {

        }

        // LogOut Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        // Profile Button 
        private void btnProfile_Click(object sender, EventArgs e)
        {
            JobSeekerProfile form = new JobSeekerProfile(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Application Button
        private void btnApplication_Click(object sender, EventArgs e)
        {
            //
        }

        // Jobs Button Current page 
        private void btnJob_Click(object sender, EventArgs e)
        {
            //
        }

       
    }
}
