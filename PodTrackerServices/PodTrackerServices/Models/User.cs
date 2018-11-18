using PodTrackerServices.podtrackdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodTrackerServices.Models
{
    public class User
    {
        public PodUser PodUser { get; set; }
        public string Token { get; set; }
    }
}
