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
using Online_Job_Management.BLL;
using Online_Job_Management.BLL.AdminBLL;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Admin
{
    public partial class AddUser : Form
    {

        private AdminBLL adminBLL = new AdminBLL();

        public AddUser()
        {
            InitializeComponent();
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

        // Job Button
        private void btnJob_Click(object sender, EventArgs e)
        {
            AllJobs allJobs = new AllJobs();
            allJobs.Show();
            this.Close();
        }
        // Logout Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        // Add Button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Retrieve input values
            string username = txtUsername.Text.Trim();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();
            string phone = txtPhone.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(role) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error");
                return;
            }

            // Add the user
            bool isAdded = adminBLL.AddUser(username, name, email, password, role, phone);
            if (isAdded)
            {
                MessageBox.Show("User added successfully.", "Success");
                AllUsers allUsers = new AllUsers();
                allUsers.Show();
                this.Close(); // Close the form
            }
            else
            {
                MessageBox.Show("Failed to add user. Please try again.", "Error");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
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