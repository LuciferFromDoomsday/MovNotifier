using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MovNotifier.Models;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovNotifier.Services
{
    public class UpcomingMovies
    {
       

        public UpcomingMovies()
        {
          
          
        }

        public SearchContainerWithDates<SearchMovie> getUpcomingMovies()
        {
            TMDbClient client = new TMDbClient("82bb3b5cf7b870891c07d7d362fe888a");
            SearchContainerWithDates<SearchMovie> sr = client.GetMovieUpcomingListAsync().Result;
            foreach (SearchMovie m in sr.Results)
            {
                Console.WriteLine(m.Video);
                Console.WriteLine(m.PosterPath);
               
            }
            return sr;

        }

    }
}
