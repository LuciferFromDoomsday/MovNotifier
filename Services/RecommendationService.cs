using System;
using System.Collections.Generic;
using System.Linq;
using MovNotifier.Models;
using Npgsql;

namespace MovNotifier.Services
{
    public class RecommendationService
    {
        private int user_id = CurrentUser.getCurrentUser().Id;

        private List<Genre> favoriteGenres;

        private List<Actor> favoriteActors;

        private List<Director> favoriteDirectors;

        private List<Movie> recommendations;
        readonly private MovieContext _context = new MovieContext();



        public List<Genre> getFavoriteGenres()
        {
            return favoriteGenres;
        }

        public List<Actor> getFavoriteActors() {
            return favoriteActors;
        }

        public List<Director> getFavoriteDirectors()
        {
            return favoriteDirectors;
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
            movies.Reverse();
            return movies.ToList();
        }

        public void GetMoviesByFavotireGenre()
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
            this.favoriteGenres = favoriteGenres;
            

        }

        public void GetMoviesByFavotireActor()
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
            this.favoriteActors = favoriteActors;
          

        }


        public List<Movie> GetMoviesByHighRating()
        {
            List<Movie> movies = GetMoviesOfUser();

            MovieContext context = new MovieContext();

            List<Movie> allMov = new List<Movie>();
            foreach (Movie m in movies)
            {

                if (m.AvgRating > 6.0)
                {
                    allMov.Add(m);
                }



            }

            return allMov.ToList();

        }



        public void GetMoviesByFavotireDirector()
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

            this.favoriteDirectors = favoriteDirectors;
            conn.Close();

        }


        public Movie GetRecommendation() {

            return recommendations.First();

        }




        public List<Movie> GetRecommendations()
        {
            if (GetMoviesOfUser() != null && GetMoviesOfUser().Count > 0 )
            {
                List<Movie> movies = GetMoviesOfUser();
                GetMoviesByFavotireActor();
                GetMoviesByFavotireDirector();
                GetMoviesByFavotireGenre();

                //foreach (Genre g in favoriteGenres) {
                //    Console.WriteLine(g.Name);
                //        }
                //foreach (Actor a in favoriteActors)
                //{
                //    Console.WriteLine(a.name);
                //}
                //foreach (Director d in favoriteDirectors)
                //{
                //    Console.WriteLine(d.name);
                //}

                List<Movie> nonUserMovies = _context.Movies.ToList();

                List<Movie> recommendations = new List<Movie>();
                foreach (Movie m in movies.ToList())
                {
                    if (nonUserMovies.Contains(m))
                    {
                        nonUserMovies.Remove(m);
                    }


                }

                foreach (Movie m in nonUserMovies)
                {
                    foreach(Director d in favoriteDirectors)
                    {
                        try
                        {
                           
                            if (d.name == MovieDbUtilities.GetDirector(m.Id).name)
                            {
                                Console.WriteLine("Added by direcotr");
                                recommendations.Add(m);
                            }
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }

                    }



          

                    foreach (Actor a in MovieDbUtilities.GetActors(m.Id))
                    {
                        foreach (Actor ac in favoriteActors)
                        {
                            if (a.name == ac.name)
                            {
                                Console.WriteLine("Added");
                                recommendations.Add(m);
                            }
                        }

                    }




                }
                foreach (Movie m in nonUserMovies)
                {
                    foreach (Genre g in MovieDbUtilities.GetGenres(m.Id).ToList())
                    {
                        foreach (Genre genre in favoriteGenres)
                        {
                            if (genre.Name == g.Name)
                            {
                                Console.WriteLine("Added");
                                recommendations.Add(m);
                            }

                        }


                    }
                }
                    //foreach(var result in recommendations.GroupBy(i => i)) {
                    //    Console.WriteLine("{0} : {1}", result.Key.Title, result.Count());
                    //}

                    recommendations = recommendations.GroupBy(x => x.Title).Select(x => x.First()).ToList();
                


                if (recommendations.Count() < 5)
                {
                    foreach (Movie m in nonUserMovies) {

                        if (recommendations.Count() > 8)
                        {

                            if (!recommendations.Contains(m) && m.AvgRating > 8)
                            {
                                recommendations.Add(m);
                            }
                        }
                        else {
                            break;
                        }


                    }

                }
                recommendations = recommendations.Distinct().ToList();
                this.recommendations = recommendations;
                return recommendations ;
            }
            else {

                this.recommendations = _context.Movies.Where(s => s.AvgRating > 8.5).ToList();
                return _context.Movies.Where(s => s.AvgRating > 8.5).ToList();


            }


        }


    }
}

