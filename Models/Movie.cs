using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovNotifier.Models
{
    public class Movie
        {
            [Key]
            public int Id { get; }

             [StringLength(255)]
             [Required]
            public string Title { get; set; }
            public  IList<ListActors> Actors { get; set; }
        
        public virtual Director Directors { get; set; }
        [StringLength(10000)]
        public string Poster { get; set; }
        [StringLength(255)]
        [Required]
        public string Runtime { get; set; }
        [StringLength(255)]
        [Required]
        public string ReleasDate { get; set; }
            public  IList<ListGenres> Genres { get; set; }
            public  Country Country { get; set; }
            public IList<ListLanguages> Languages { get; set; }
        
        [Required]
        public double AvgRating { get; set; }
        [StringLength(10000)]
        [Required]
        public string Description { get; set; }

        public IList<ListRecommendations> Users { get; set; }

        public Movie()
            {



            }
            public Movie(int id, string title, string poster, string runtime, string releasedDate, Country made_country, double avgRating)
            {

                this.Id = id;
                this.Title = title;
                this.Poster = poster;
                this.Runtime = runtime;
                this.ReleasDate = releasedDate;
                this.Country = made_country;
                this.AvgRating = avgRating;

            }
        }
}

