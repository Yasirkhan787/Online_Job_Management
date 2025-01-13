using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Job_Management.Model
{
        public class JobSeeker
        {
            public int JobSeekerID { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Role { get; set; }
        }
    }

