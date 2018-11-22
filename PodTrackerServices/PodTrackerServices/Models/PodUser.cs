using System;
using System.Collections.Generic;

namespace PodTrackerServices.Models
{
    public partial class PodUser
    {
        public PodUser()
        {
            FollowedPodcast = new HashSet<FollowedPodcast>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<FollowedPodcast> FollowedPodcast { get; set; }
    }
}
