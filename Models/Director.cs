using System;
using System.ComponentModel.DataAnnotations;

namespace MovNotifier.Models
{
    public class Director
    {
        public int Id { get; set; }
        [StringLength(255)]
        [Required]
        public string name { get; set; }
        public Director()
        {
        }
        public Director(int id, string name)
        {
            this.Id = id;
            this.name = name;

        }
    }
}
