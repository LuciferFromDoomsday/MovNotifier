using System;
namespace MovNotifier.Models
{
    public class ListRecommendations
    {

        public int UserId { get; set; }
        public User User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }


        public ListRecommendations()
        {
        }
    }
}
