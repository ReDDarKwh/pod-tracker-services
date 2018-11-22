using System;
using System.Collections.Generic;

namespace PodTrackerServices.Models
{
    public partial class PodcastEpisode
    {
        public int Id { get; set; }
        public string AudioUrl { get; set; }
        public int? TimeStep { get; set; }
        public int? FollowedPodcastId { get; set; }

        

    }
}
