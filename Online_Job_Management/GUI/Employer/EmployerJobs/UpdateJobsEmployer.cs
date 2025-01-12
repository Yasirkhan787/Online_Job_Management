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

using Online_Job_Management.GUI.SecurityManagement;

using Online_Job_Management.Model;
using Online_Job_Management.Session;

namespace Online_Job_Management.GUI.Employer.EmployerJobs
{
    public partial class UpdateJobsEmployer : Form
    {
        private EmployerBLL employerBLL;
        private Job jobToUpdate;
        private CategoryBLL categoryBLL;
        private int employerId;
        public UpdateJobsEmployer(int employerId)
        {
            InitializeComponent();
            employerBLL = new EmployerBLL();
            categoryBLL = new CategoryBLL();
            jobToUpdate = new Job();
            LoadJobDetails();
            LoadCategories();
            this.employerId = employerId;
        }


        // Load Categories 
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

        // Load job details into the form fields
        private void LoadJobDetails()
        {
            txtTitle.Text = jobToUpdate.Title;
            textDescription.Text = jobToUpdate.Description;
            textRequirements.Text = jobToUpdate.Requirements;
            txtLocation.Text = jobToUpdate.Location;
            txtSalary.Text = jobToUpdate.Salary.ToString();
            txtType.Text = jobToUpdate.JobType;
            txtCompany.Text = jobToUpdate.CompanyName;
            cmbCategory.Items.Clear();

        }
        // Update Button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Update job details with values from form fields
                jobToUpdate.Title = txtTitle.Text;
                jobToUpdate.Description = textDescription.Text;
                jobToUpdate.Requirements = textRequirements.Text;
                jobToUpdate.Location = txtLocation.Text;
                jobToUpdate.CompanyName = txtCompany.Text;
                jobToUpdate.Salary = decimal.Parse(txtSalary.Text);
                jobToUpdate.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                jobToUpdate.JobType = txtType.Text;


                // Get CategoryID from the ComboBox (selected value)
                if (cmbCategory.SelectedItem != null)
                {
                    // Extract the CategoryID from the selected ComboBox item
                    jobToUpdate.CategoryID = ((KeyValuePair<int, string>)cmbCategory.SelectedItem).Key;
                }
                else
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }

                // Call the BLL to update the job in the database
                bool isUpdated = employerBLL.UpdateJob(jobToUpdate);

                if (isUpdated)
                {
                    MessageBox.Show("Job updated successfully.");
                    this.Close(); // Close the form after saving
                }
                else
                {
                    MessageBox.Show("Failed to update job. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //Application Button 
        private void btnApplications_Click_1(object sender, EventArgs e)
        {
            EmployerDashboard form = new EmployerDashboard(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Job
        private void btnJobs_Click(object sender, EventArgs e)
        {
            AllJobsEmployer form = new AllJobsEmployer(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Profile
        private void btnProfile_Click(object sender, EventArgs e)
        {
            //form.Show();
            //this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void UpdateJobsEmployer_Load(object sender, EventArgs e)
        {

        }
    }
}
