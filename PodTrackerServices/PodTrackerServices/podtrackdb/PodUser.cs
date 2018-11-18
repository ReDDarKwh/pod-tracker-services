using System;
using System.Collections.Generic;

namespace PodTrackerServices.podtrackdb
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

        public ICollection<FollowedPodcast> FollowedPodcast { get; set; }
    }
}
