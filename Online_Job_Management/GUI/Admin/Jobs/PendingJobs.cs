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
using Online_Job_Management.GUI;
using Online_Job_Management.GUI.Admin;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace JobApp.GUI.Admin.Jobs
{
    public partial class PendingJobs : Form
    {
        private AdminBLL adminBLL = new AdminBLL();

        public PendingJobs()
        {
            InitializeComponent();
            LoadJobs();
        }

        // Method to load all Pending jobs and bind to DataGridView
        private void LoadJobs()
        {
            try
            {
                // Fetch data from BLL which calls DAL to get data from the DB Bind the data to the DataGridView
                dgvPendings.DataSource = adminBLL.GetPendingJobs();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
        private void dgvPendings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvPendings.Columns[e.ColumnIndex].HeaderText;


            }
        }
        private void PendingJobs_Load(object sender, EventArgs e)
        {

        }
        // Dashboard Button
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();
            this.Close();
        }
        // Users Button
        private void btnUsers_Click_1(object sender, EventArgs e)
        {
            AllUsers users = new AllUsers();
            users.Show();
            this.Close();
        }
        // Pending Jobs (Current Page)
        private void btnPending_Click(object sender, EventArgs e)
        {

        }
        // Log Out Button
        private void button1_Click_1(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }
        

        // Job Button
        private void btnJob_Click(object sender, EventArgs e)
        {
            AllJobs allJobs = new AllJobs();    
            allJobs.Show();
            this.Close();
        }

    }
}
