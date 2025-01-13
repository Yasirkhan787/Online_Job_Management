using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Job_Management.GUI.JobSeeker
{
    public partial class ApplyJobForm : Form
    {
        int jobseekerID;
        public ApplyJobForm(int jobseekerID)
        {
            InitializeComponent();
            this.jobseekerID = jobseekerID;
        }
    }
}
