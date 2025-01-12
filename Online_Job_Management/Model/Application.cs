using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Job_Management.Model
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int JobSeekerID { get; set; }
        public string ResumePath { get; set; }
        public string Status { get; set; } // Pending, Shortlisted, Rejected, Hired
        public DateTime ApplyDate { get; set; }
        public string JobTitle { get; set; }
        public string ApplicantName { get; set; }
    }
}
