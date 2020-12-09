using System;
using System.Collections.Generic;

namespace MovNotifier.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string name { get; set; }
        public IList<ListLanguages> Movies { get; set; }
        public Language()
        {
        }
    }
}
