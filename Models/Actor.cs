using System;
using System.Collections.Generic;

namespace MovNotifier.Models
{
    public class Actor
    {

        public int Id { get; set; }
        public string name { get; set; }
        public IList<ListActors> Movies { get; set; }
        public Actor()
        {
        }
        public Actor(int id, string name)
        {
            this.Id = id;
            this.name = name;
        }
    }
}
