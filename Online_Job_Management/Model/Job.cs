using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Job_Management.Model
{
    public class Job
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EmployerID { get; set; }
        public int CategoryID { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string Requirements { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
    }

}

