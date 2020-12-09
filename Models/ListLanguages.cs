using System;
namespace MovNotifier.Models
{
    public class ListLanguages
    {

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public ListLanguages()
        {
        }
    }
}
