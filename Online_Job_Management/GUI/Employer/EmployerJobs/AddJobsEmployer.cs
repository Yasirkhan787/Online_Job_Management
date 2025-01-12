using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL;
using Online_Job_Management.BLL.EmployerBLL;
using Online_Job_Management.GUI.Employer.EmployerJobs;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Employer
{
    public partial class AddJobsEmployer : Form
    {
        private EmployerBLL employerBLL = new EmployerBLL();

        private CategoryBLL categoryBLL = new CategoryBLL();
        private int employerId;

        public AddJobsEmployer(int employerId)
        {
            InitializeComponent();
            this.employerId = employerId;
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {

                // Fetch categories from BLL
                List<Category> categories = categoryBLL.GetAllCategories();

                // Bind categories to ComboBox
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name"; // Display category name
                cmbCategory.ValueMember = "CategoryID"; // Use CategoryID as the value

                // Clear any existing selection
                cmbCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load categories. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add Job Button Click Event
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(textDescription.Text) ||
                    cmbCategory.SelectedValue == null ||
                    string.IsNullOrWhiteSpace(txtLocation.Text) ||
                    string.IsNullOrWhiteSpace(txtSalary.Text) ||
                    string.IsNullOrWhiteSpace(textRequirements.Text) ||
                    string.IsNullOrWhiteSpace(txtCompany.Text) ||
                    string.IsNullOrWhiteSpace(txtType.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create Job object
                Job job = new Job
                {
                    Title = txtTitle.Text,
                    Description = textDescription.Text,
                    EmployerID = employerId,
                    CategoryID = Convert.ToInt32(cmbCategory.SelectedValue),
                    Location = txtLocation.Text,
                    Salary = Convert.ToDecimal(txtSalary.Text),
                    Requirements = textRequirements.Text,
                    CompanyName = txtCompany.Text,
                    JobType = txtType.Text,
                    Status = "Pending"
                };

                // Add job through BLL
                if (employerBLL.AddJob(job))
                {
                    MessageBox.Show("Job added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Navigate to All Jobs form
                    AllJobsEmployer jobs = new AllJobsEmployer(employerId);
                    jobs.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add job. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid data format. Please check your inputs (e.g., Salary should be numeric).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Application Button 

        private void btnApplications_Click(object sender, EventArgs e)
        {
            EmployerDashboard form = new EmployerDashboard(UserSession.UserID);
            form.Show();
            this.Close();
        }


        // Jobs Button Click Event

        private void btnJobs_Click(object sender, EventArgs e)
        {
            AllJobsEmployer form = new AllJobsEmployer(UserSession.UserID);
            form.Show();
            this.Close();
        }


        // Profile Button Click Event
        private void btnProfile_Click(object sender, EventArgs e)
        {
            // Implement profile navigation if required
            MessageBox.Show("Profile section under construction.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Logout Button Click Event
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }
    }
}
