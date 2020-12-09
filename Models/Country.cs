using System;
namespace MovNotifier.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string timeZone { get; set; }
        public Country()
        {
        }
        public Country(int id, string name, string timeZone)
        {
            this.ID = id;
            this.name = name;
            this.timeZone = timeZone;
        }
    }

}
