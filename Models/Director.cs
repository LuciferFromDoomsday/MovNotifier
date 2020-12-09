using System;
namespace MovNotifier.Models
{
    public class Director
    {
        public int Id { get; set; }
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
