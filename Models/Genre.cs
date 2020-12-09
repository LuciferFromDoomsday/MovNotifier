using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovNotifier.Models
{
    public class Genre
    {

        public int Id { get; set; }
      
        public string Name { get; set; }
        public IList<ListGenres> Movies { get; set; }
        public Genre()
        {
        }
    }
}
