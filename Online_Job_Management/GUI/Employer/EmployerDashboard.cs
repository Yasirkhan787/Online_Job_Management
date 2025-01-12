using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Online_Job_Management.BLL.EmployerBLL;
using Online_Job_Management.GUI.Employer.EmployerJobs;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Model;
using Online_Job_Management.Session;
using Application = Online_Job_Management.Model.Application;

namespace Online_Job_Management.GUI.Employer
{
    public partial class EmployerDashboard : Form
    {
        private EmployerBLL employerBLL = new EmployerBLL();
        private int employerID;

        public EmployerDashboard(int employerID)
        {
            InitializeComponent();
            this.employerID = employerID;
            LoadApplications();
        }

        private void LoadApplications()
        {
            List<Application> applications = employerBLL.GetAllApplications(employerID);
            dgvApplications.DataSource = applications;

            // Add Action Buttons
            AddActionButtons();
        }

        private void AddActionButtons()
        {
            if (!dgvApplications.Columns.Contains("Approve"))
            {
                DataGridViewButtonColumn approveButton = new DataGridViewButtonColumn();
                approveButton.Name = "Approve";
                approveButton.Text = "Approve";
                approveButton.UseColumnTextForButtonValue = true;
                dgvApplications.Columns.Add(approveButton);
            }

            if (!dgvApplications.Columns.Contains("Reject"))
            {
                DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
                rejectButton.Name = "Reject";
                rejectButton.Text = "Reject";
                rejectButton.UseColumnTextForButtonValue = true;
                dgvApplications.Columns.Add(rejectButton);
            }
        }
        //
        private void dgvApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = dgvApplications.Columns[e.ColumnIndex].HeaderText;
                int applicationID = Convert.ToInt32(dgvApplications.Rows[e.RowIndex].Cells["ApplicationID"].Value);

                if (columnName == "Approve")
                {
                    if (employerBLL.UpdateApplicationStatus(applicationID, "Approved"))
                    {
                        MessageBox.Show("Application approved successfully.");
                        LoadApplications();
                    }
                }
                else if (columnName == "Reject")
                {
                    if (employerBLL.UpdateApplicationStatus(applicationID, "Rejected"))
                    {
                        MessageBox.Show("Application rejected.");
                        LoadApplications();
                    }
                }
            }
        }
        // Application (Current Page)
        private void btnApplications_Click(object sender, EventArgs e)
        {

        }

        // Jobs Button 
        private void btnJobs_Click(object sender, EventArgs e)
        {
            AllJobsEmployer form = new AllJobsEmployer(UserSession.UserID);
            form.Show();
            this.Close();
        }

        // Profile Button 
        private void btnProfile_Click(object sender, EventArgs e)
        {

        }

        // Logout Button
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        
    }
}
