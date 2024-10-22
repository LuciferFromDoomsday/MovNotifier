﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovNotifier.Models
{
    public class Actor
    {

        public int Id { get; set; }
        [StringLength(255)]
        [Required]
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
