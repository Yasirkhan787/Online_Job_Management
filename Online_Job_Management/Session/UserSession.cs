using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Job_Management.Session
{
    public static class UserSession
    {
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            UserID = 0;
            Username = null;
            Name = null;
            Email = null;
            Role = null;
        }
    }

}
