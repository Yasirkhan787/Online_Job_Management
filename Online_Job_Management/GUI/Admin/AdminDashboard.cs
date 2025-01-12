using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobApp.GUI.Admin.Jobs;
using Online_Job_Management.BLL.AdminBLL;
using Online_Job_Management.GUI.Admin;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI
{
    public partial class AdminDashboard : Form
    {

        private AdminBLL adminBLL= new AdminBLL();

        public AdminDashboard()
        {
            InitializeComponent();
            LoadDashboard();
        }

        // Load Dashboard
        private void LoadDashboard()
        {
            // Example: Set dashboard labels
            lblTotalUsers.Text = adminBLL.GetAllUsers().Rows.Count.ToString();
            lblTotalJobs.Text = adminBLL.GetAllJobs().Count.ToString(); 
            lblPendingJobs.Text = adminBLL.GetPendingJobs().Rows.Count.ToString();
        }


        // Dashboard Button (Current page)
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            
        }

        // Users Button
        private void btnUsers_Click(object sender, EventArgs e)
        {
            AllUsers users = new AllUsers();
            users.Show();
            this.Close();
        }

        // Jobs Button
        private void btnJob_Click(object sender, EventArgs e)
        {
            AllJobs allJobs = new AllJobs();
            allJobs.Show(); 
            this.Close();
        }

        // Log Out Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            PendingJobs pendingJobs = new PendingJobs();
            pendingJobs.Show();
            this.Close();
        }
    }
}
