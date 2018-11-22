
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodTrackerServices.Models
{
    public class User
    {
       
        public string Token { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
