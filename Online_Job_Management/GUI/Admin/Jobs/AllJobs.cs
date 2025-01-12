using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL;
using Online_Job_Management.DAL;
using Online_Job_Management.Model;
using Online_Job_Management.GUI;
using Online_Job_Management.GUI.Admin;
using Online_Job_Management.GUI.Employer;
using Online_Job_Management.Session;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.BLL.AdminBLL;

namespace JobApp.GUI.Admin.Jobs
{
    public partial class AllJobs : Form
    {
        private AdminBLL adminBLL = new AdminBLL();

        public AllJobs()
        {
            InitializeComponent();
            LoadJobs();
        }


        // Dashboard Button
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();
            this.Close();
        }

        // Users Button
        private void btnUsers_Click(object sender, EventArgs e)
        {
            AllUsers users = new AllUsers();    
            users.Show();
            this.Close();
        }

        // Jobs Button ( Current Page)
        private void btnJob_Click(object sender, EventArgs e)
        {
            
        }

        // Log Out Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }


        // Method to load all jobs and bind to DataGridView
        private void LoadJobs()
        {
            try
            {
                // Fetch data from BLL which calls DAL to get data from the DB
                List<Job> jobs = adminBLL.GetAllJobs();

                // Bind the data to the DataGridView
                dgvJobs.DataSource = jobs;

                // Add Action Buttons  to DataGridView
                AddActionButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        // Add action buttons to DataGridView 
        private void AddActionButtons()
        {
            //
            if (!dgvJobs.Columns.Contains("Approve"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "Approve";
                editButtonColumn.Text = "Approve";
                editButtonColumn.UseColumnTextForButtonValue = true;
                dgvJobs.Columns.Add(editButtonColumn);
            }

            // 
            if (!dgvJobs.Columns.Contains("Reject"))
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.Name = "Reject";
                deleteButtonColumn.Text = "Reject";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dgvJobs.Columns.Add(deleteButtonColumn);
            }
        }

        //  Handle actions  buttons in DataGridView
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvJobs.Columns[e.ColumnIndex].HeaderText;

                if (columnName == "Approve")
                {
                    // Handle Edit logic for job
                    int jobID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value);


                    // Approve the Job
                    adminBLL.ApproveJob(jobID);
                    MessageBox.Show("Job Rejected successfully.");
                    LoadJobs(); // Refresh the DataGridView after editing
                }
                else if (columnName == "Reject")
                {
                    // Handle Delete logic for job
                    int jobID = (int)dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value;

                    // Call the BLL method to Reject the Job
                    adminBLL.RejectJob(jobID);
                    MessageBox.Show("Job Rejected successfully.");
                    LoadJobs(); // Refresh the DataGridView
               
                }
            }
        }

        private void AllJobs_Load(object sender, EventArgs e)
        {

        }

        private void btnPending_Click_1(object sender, EventArgs e)
        {
            PendingJobs pendingJobs = new PendingJobs();
            pendingJobs.Show();
            this.Close();
        }
    }
}


