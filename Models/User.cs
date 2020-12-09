using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MovNotifier.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Country Country { get; set; }
        public int Age { get; set; }
        public Language Language { get; set; }
        public string Email { get; set; }
        public IList<ListRecommendations> Movies { get; set; }
        public IList<ListWatchedMovies> WatchedMovies { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public User()
        {
        }

        public bool isInRole(string role)
        {
            if(this.Role == role)
            {
                return true;
            }
            return false;
        }


        public User( string name, string surname, Country country, int age, Language language)
        {
            
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
            this.Country = country;
            this.Language = language;

        }
    }
}
