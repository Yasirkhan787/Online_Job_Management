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
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Admin
{
    
    public partial class UpdateUser : Form
    {
        private AdminBLL adminBLL;

        // Attributes
        private int UserID;
        private string Username;
        private string Name;
        private string Email;
        private string Password;
        private string Role;
        private string Phone;


        public UpdateUser(int userID, string username, string name, string email, string password, string role, string phone)
        {
            InitializeComponent();

            adminBLL = new AdminBLL();

            // Assign the parameters to class attributes
            this.UserID = userID;
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.Phone = phone;

            // Populate the fields with existing data
            txtUsername.Text = Username;
            txtName.Text = Name;
            txtEmail.Text = Email;
            txtPassword.Text = Password;
            cmbRole.Text = Role;
            txtPhone.Text = Phone;

        }
        // Update Button
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

            // Call the BLL method to update the User
            bool isAdded = adminBLL.UpdateUser(UserID, username, email, name, role, phone);
            if (isAdded)
            { 
                MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 // Clear the input fields

                // Redirect to AirplaneForm
                AllUsers form = new AllUsers();
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Log Out Button 
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        // Jobs Button

        // 
        private void UpdateUser_Load(object sender, EventArgs e)
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
