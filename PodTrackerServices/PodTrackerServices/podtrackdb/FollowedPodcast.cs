using System;
using System.Collections.Generic;

namespace PodTrackerServices.podtrackdb
{
    public partial class FollowedPodcast
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TimeStep { get; set; }
        public string Rss { get; set; }

        public PodUser User { get; set; }
    }
}
