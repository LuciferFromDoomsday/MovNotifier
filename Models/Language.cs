using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MovNotifier.Utilities;

namespace MovNotifier.Models
{
    public class Language
    {
        public int Id { get; set; }
        [StringLength(255)]
        [Required]
        [MakeMeUpperCase]
        public string name { get; set; }
        public IList<ListLanguages> Movies { get; set; }
        public Language()
        {
        }
    }
}
