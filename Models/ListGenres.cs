using System;
namespace MovNotifier.Models
{
    public class ListGenres
    {


        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }


        public ListGenres()
        {
        }
    }
}
