using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Online_Job_Management.BLL.AdminBLL;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.JobSeeker
{
    
public partial class JobSeekerProfile : Form
    {
        private AdminBLL adminBLL = new AdminBLL();
        int jobSeekerID;
        public JobSeekerProfile(int jobSeekerID)
        {
            InitializeComponent();
            this.jobSeekerID = jobSeekerID;
            LoadProfile();
        }
        private void LoadProfile()
        {
            try
            {
                // Example: Fetch jobseeker details based on jobSeekerID

                User jobSeeker = adminBLL.GetUserByID(jobSeekerID); // Change JobSeeker to User
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

        // Job Button
        private void btnJob_Click(object sender, EventArgs e)
        {
            JobSeekerDashboard form = new JobSeekerDashboard();
            form.Show();
            this.Close();
        }


        // Profile Current 
        private void btnProfile_Click(object sender, EventArgs e)
        {
            
        }

        // LogOut Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void JobSeekerProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
