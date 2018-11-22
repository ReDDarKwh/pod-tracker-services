using System;
using System.Collections.Generic;

namespace PodTrackerServices.Models
{
    public partial class FollowedPodcast
    {
        public FollowedPodcast()
        {
            PodcastEpisode = new HashSet<PodcastEpisode>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Rss { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool Followed { get; set; }
        public DateTime? LastListened { get; set; }

        public virtual PodUser User { get; set; }
        public virtual ICollection<PodcastEpisode> PodcastEpisode { get; set; }
    }
}
