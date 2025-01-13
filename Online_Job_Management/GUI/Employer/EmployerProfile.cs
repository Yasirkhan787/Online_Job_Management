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
using Online_Job_Management.GUI.Employer.EmployerJobs;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Employer
{
    public partial class EmployerProfile : Form
    {
        private AdminBLL adminBLL = new AdminBLL();
        int employerID;
        public EmployerProfile(int employerID)
        {
            InitializeComponent();
            
            this.employerID = employerID;
            LoadProfile();
        }
        private void LoadProfile()
        {
            try
            {
                // Example: Fetch jobseeker details based on jobSeekerID

                User jobSeeker = adminBLL.GetUserByID(employerID); // Change JobSeeker to User
                if (jobSeeker != null)
                {
                    txtUsername.Text = jobSeeker.Username;
                    txtEmail.Text = jobSeeker.Email;
                    txtName.Text = jobSeeker.Name;
                    txtPhone.Text = jobSeeker.Phone;
                    txtRole.Text = jobSeeker.Role;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
            }
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            EmployerDashboard form = new EmployerDashboard(UserSession.UserID);
            form.Show();
            this.Close();
        }

        private void btnJobs_Click(object sender, EventArgs e)
        {
            AllJobsEmployer form = new AllJobsEmployer(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Current 
        private void btnProfile_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void EmployerProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
