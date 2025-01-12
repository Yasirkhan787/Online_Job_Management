using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL.UserBLL;
using Online_Job_Management.GUI.Employer;

using Online_Job_Management.GUI.JobSeeker;

using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.SecurityManagement
{
    public partial class LoginForm : Form
    {
        // Creating Object of UserBLL Class 
        private UserBLL userBLL = new UserBLL();


        // Constructor 
        public LoginForm()
        {
            InitializeComponent();
        }

        // Method to Handle Login Button Click Event 
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow user = userBLL.Login(username, password);

            if (user != null)
            {
                // Populate the UserSession
                UserSession.UserID = Convert.ToInt32(user["UserID"]);
                UserSession.Name = user["Name"].ToString();
                UserSession.Username = user["Username"].ToString();
                UserSession.Role = user["Role"].ToString();

                MessageBox.Show($"Welcome, {UserSession.Name}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Navigate to respective dashboard
                switch (UserSession.Role)
                {
                    case "Admin":
                        new AdminDashboard().Show();
                        break;
                    case "Employer":
                        new EmployerDashboard(UserSession.UserID).Show();
                        break;
                    case "Job Seeker":
                        new JobSeekerDashboard().Show();
                        break;
                    default:
                        MessageBox.Show("Unknown role. Please contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to Handle Register Button Click Event
        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}

