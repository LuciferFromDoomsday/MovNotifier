using System;
using System.Collections.Generic;
using System.Linq;
using MovNotifier.Models;
using Npgsql;

namespace MovNotifier.Services
{
    public class RecommendationService
    {
        private int user_id;

        static private List<Genre> favoriteGenres ;

        static private List<Actor> favoriteActors  ;

        static  private List<Director> favoriteDirectors ;

       
        readonly private MovieContext _context = new MovieContext();
        public RecommendationService(int id)
        {
            this.user_id = id;
        }


        public List<Movie> GetMoviesOfUser() {

            List<Movie> movies = new List<Movie>();

            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();

            NpgsqlCommand command = new NpgsqlCommand("SELECT movie_id from user_history where user_id = " + user_id, conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {

                if (dr[0] != null)
                {
                    try
                    {
                        movies.Add(_context.Movies.Where(s => s.Id == (int)dr[0]).First());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }



            conn.Close();

            return movies.ToList();
        }

        public List<Movie> GetMoviesByFavotireGenre()
        {
            List<Movie> movies = GetMoviesOfUser();

            MovieContext context = new MovieContext();
            List<Genre> favoriteGenres = new List<Genre>();

            List<Genre> allGenres = new List<Genre>();
            foreach (Movie m in movies)
            {

                List<ListGenres> listGenres = _context.ListGenres.Where(s => s.GenreId == m.Id).ToList();

                foreach (ListGenres lg in listGenres) {

                    allGenres.Add(_context.Genres.Where(s => s.Id == lg.MovieId).First());

                }



            }
            int count = allGenres.GroupBy(i => i).Max(s => s.Count());
            foreach (var result in allGenres.GroupBy(i => i))
            {
                if (count == result.Count())
                {
                    favoriteGenres.Add(result.Key);

                    Console.WriteLine("{0} : {1}", result.Key.Name, result.Count());
                }


            }
            RecommendationService.favoriteGenres = favoriteGenres;
            return movies.ToList();

        }

        public List<Movie> GetMoviesByFavotireActor()
        {
            List<Movie> movies = GetMoviesOfUser();

            MovieContext context = new MovieContext();

            List<Actor> allActors = new List<Actor>();
            List<Actor> favoriteActors = new List<Actor>();
            foreach (Movie m in movies)
            {

                List<ListActors> listActors = _context.ListActors.Where(s => s.ActorId == m.Id).ToList();

                foreach (ListActors lg in listActors)
                {

                    allActors.Add(_context.Actors.Where(s => s.Id == lg.MovieId).First());

                }



            }
            int count = allActors.GroupBy(i => i).Max(s => s.Count());

            foreach (var result in allActors.GroupBy(i => i))
            {
                if (count == result.Count())
                {
                    favoriteActors.Add(result.Key);
                    Console.WriteLine("{0} : {1}", result.Key.name, result.Count());
                }
            }
            RecommendationService.favoriteActors = favoriteActors;
            return movies.ToList();

        }


        public List<Movie> GetMoviesByHighRating()
        {
            List<Movie> movies = GetMoviesOfUser();

            MovieContext context = new MovieContext();

            List<Movie> allMov = new List<Movie>();
            foreach (Movie m in movies)
            {

          if(m.AvgRating > 6.0)
                {
                    allMov.Add(m);
                }



            }

            return allMov.ToList();

        }



        public List<Movie> GetMoviesByFavotireDirector()
        {
            List<Movie> movies = GetMoviesOfUser();

            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();



            MovieContext context = new MovieContext();
            List<Director> favoriteDirectors = new List<Director>();
            List<Director> allDirectors = new List<Director>();
            foreach (Movie m in movies.ToList())
            {


                NpgsqlCommand command = new NpgsqlCommand(@"SELECT ""DirectorsId"" from public.""Movies""  where ""Id"" = " + m.Id, conn);

                // Execute the query and obtain a result set
                using (NpgsqlDataReader dr = command.ExecuteReader())
                {

                    if (dr.Read())
                    {

                        if (dr[0] != null)
                        {
                            try
                            {
                                allDirectors.Add(_context.Directors.Where(s => s.Id == (int)dr[0]).First());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                    }
                }

            }

            int count = allDirectors.GroupBy(i => i).Max(s => s.Count());

            foreach (var result in allDirectors.GroupBy(i => i))
            {
                if (count == result.Count())
                {
                    favoriteDirectors.Add(result.Key);
                    Console.WriteLine("{0} : {1}", result.Key.name, result.Count());
                }
            }
            RecommendationService.favoriteDirectors = favoriteDirectors;
            return movies.ToList();

        }


    }
   }

