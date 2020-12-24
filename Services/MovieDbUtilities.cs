using System;
using System.Collections.Generic;
using System.Linq;
using MovNotifier.Models;
using Npgsql;

namespace MovNotifier.Services
{
    public static class MovieDbUtilities
    {
        static NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
        static MovieContext _context = new MovieContext();

        public static Director GetDirector(int movie_id)
        {

            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();
            NpgsqlCommand command = new NpgsqlCommand(@"SELECT ""DirectorsId"" from public.""Movies""  where ""Id"" = " + movie_id, conn);

            using (NpgsqlDataReader dr = command.ExecuteReader())
            {

                if (dr.Read())
                {

                    if (dr[0] != null)
                    {
                        
                           
                           
                        Director d = _context.Directors.Where(s => s.Id == (int)dr[0]).First();
                        conn.Close();
                        return d;

                   

                    }
                    
                }
                
            }
            conn.Close();
            return null;
        }



        public static Country GetCountry(int movie_id)
        {

            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();
            NpgsqlCommand command = new NpgsqlCommand(@"SELECT ""CountryID"" from public.""Movies""  where ""Id"" = " + movie_id, conn);

            using (NpgsqlDataReader dr = command.ExecuteReader())
            {

                if (dr.Read())
                {

                    if (dr[0] != null)
                    {



                        Country d = _context.Countries.Where(s => s.ID == (int)dr[0]).First();
                        conn.Close();
                        return d;



                    }

                }

            }
            conn.Close();
            return null;
        }


        public static List<Language> GetLanguages(int movie_id)
        {
            List<Language> languages = new List<Language>();

            List<Language> allLanguages = new List<Language>();
            List<ListLanguages> listActors = _context.ListLanguages.Where(s => s.LanguageId == movie_id).ToList();

            foreach (ListLanguages lg in listActors)
            {

                allLanguages.Add(_context.Languages.Where(s => s.Id == lg.MovieId).First());

            }
            return allLanguages;
        }

        public static List<Actor> GetActors(int movie_id)
        {
            List<Actor> favoriteActors = new List<Actor>();

            List<Actor> allActors = new List<Actor>();
            MovieContext context = new MovieContext();
            List<ListActors> listActors = context.ListActors.Where(s => s.ActorId == movie_id).ToList();

            foreach (ListActors lg in listActors)
            {

                allActors.Add(_context.Actors.Where(s => s.Id == lg.MovieId).First());

            }
            return allActors;
        }
        public static List<Genre> GetGenres(int movie_id)
        {


            List<Genre> allGenres = new List<Genre>();
            List<ListGenres> listGenres = _context.ListGenres.Where(s => s.GenreId == movie_id).ToList();

            foreach (ListGenres lg in listGenres)
            {

                allGenres.Add(_context.Genres.Where(s => s.Id == lg.MovieId).First());

            }
            return allGenres;
        }


    }
}
