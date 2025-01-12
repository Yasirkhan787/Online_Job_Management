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
using JobApp.GUI.Admin.Jobs;
using Online_Job_Management.BLL;
using Online_Job_Management.BLL.AdminBLL;
using Online_Job_Management.GUI.SecurityManagement;
using Online_Job_Management.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Online_Job_Management.GUI.Admin
{
    public partial class AllUsers : Form
    {
        private AdminBLL adminBLL = new AdminBLL();

        public AllUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        // Method to load all Users into the DataGridView
        private void LoadUsers()
        {
            try
            {
               
                // Bind the data to DataGridView
                dgvUsers.DataSource = adminBLL.GetAllUsers(); ;

                // Add Action Buttons (Edit and Delete) after binding the data
                AddActionButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        // Action Buttons 
        private void AddActionButtons()
        {
            // Add Edit Button if not already added
            if (!dgvUsers.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "Edit";
                editButtonColumn.Text = "Edit";
                editButtonColumn.UseColumnTextForButtonValue = true;
                dgvUsers.Columns.Add(editButtonColumn);
            }

            // Add Delete Button if not already added
            if (!dgvUsers.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.Name = "Delete";
                deleteButtonColumn.Text = "Delete";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dgvUsers.Columns.Add(deleteButtonColumn);
            }
        }

        // Grid View
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvUsers.Columns[e.ColumnIndex].HeaderText;

                if (columnName == "Edit")
                {
                    // Handle Edit logic

                    // Retrieve row data
                    int UserID = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value);
                    string username = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                    string name = dgvUsers.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    string email = dgvUsers.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                    string password = dgvUsers.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                    string role = dgvUsers.Rows[e.RowIndex].Cells["Role"].Value.ToString();
                    string phone = dgvUsers.Rows[e.RowIndex].Cells["Phone"].Value.ToString();


                    // Open the Update Form and pass the data
                    UpdateUser updateUser = new UpdateUser(UserID, username, name, email, password, role, phone);
                    this.Close();
                    updateUser.ShowDialog();
                    // Refresh the DataGridView after updating (if needed)
                    LoadUsers();

                }
                else if (columnName == "Delete")
                {
                    // Handle Delete logic
                    int UserID = (int)dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value;

                    // Call the BLL method to delete the airplane
                    if (adminBLL.DeleteUser(UserID))
                    {
                        MessageBox.Show("User deleted successfully.");
                        LoadUsers(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete User.");
                    }
                }
            }
        }

        // Dashboard Button
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AdminDashboard dashboard = new AdminDashboard();
            dashboard.Show();
            this.Close();
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        // Log Out Button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Close();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            AllJobs allJobs = new AllJobs();
            allJobs.Show();
            this.Close();
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            PendingJobs pendingJobs = new PendingJobs();
            pendingJobs.Show();
            this.Close();
        }
    }
}
