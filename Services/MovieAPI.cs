using System;
using System.Collections.Generic;
using MovNotifier.Models;
using OMDbApiNet;
using OMDbApiNet.Model;

namespace MovNotifier.Services
{
    public class MovieAPI
    {
      private  OmdbClient omdb { get; set; }


        public MovieAPI()
        {
         

            // enable ratings from Rotten Tomatoes
            this.omdb = new OmdbClient("f53cef60", true);


       
        }

        public List<SearchItem> GetMoviesByTitle(string title ) {

            try
            {

                SearchList searchList = omdb.GetSearchList(title, OmdbType.Movie, 1);
                return searchList.SearchResults;
            }
            catch
            {
                return new List<SearchItem>();
            }


            

        }

        public Item GetMovieById(String id)
        {

            try
            {
              Item item =  omdb.GetItemById(id , true);

                return item;

            }
            catch {

                return null;
            }


        }

        public List<SearchItem> GetMoviesByYear(string year)
        {

            SearchList searchList = omdb.GetSearchList(2020, "Sponge", OmdbType.Movie , 1);




            return searchList.SearchResults;

        }


    }
}
