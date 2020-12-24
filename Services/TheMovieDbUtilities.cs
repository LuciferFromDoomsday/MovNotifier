using System;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using System.Collections.Generic;
using Npgsql;

namespace MovNotifier.Services
{
    public static class TheMovieDbUtilities
    {
       public static String getLinkById(int id) {

            TMDbClient client = new TMDbClient("82bb3b5cf7b870891c07d7d362fe888a");
        
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();
            //Movie movie = client.GetMovieAsync(id, MovieMethods.Images | MovieMethods.Videos).Result;
            //movie.Title = String.Join("", movie.Title.Split("'" , '-'));
            //Console.WriteLine(movie.Title);
            NpgsqlCommand command = new NpgsqlCommand("SELECT movie_trailer_link FROM movie_trailer WHERE movie_title = '" + id + "'", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                if (dr[0] != null)
                {
                    try
                    {
                        Console.WriteLine("returned with db");
                        return (string)dr[0];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }


                }

            }
            else
            {

                conn.Close();
                Movie movie = client.GetMovieAsync(id, MovieMethods.Images | MovieMethods.Videos).Result;
                movie.Title = String.Join("", movie.Title.Split("'", '-'));
                Console.WriteLine(movie.Title);
                SearchContainer<SearchMovie> results = client.SearchMovieAsync(movie.Title).Result;

    String youtubeLink = "";
    SearchMovie search = new SearchMovie();
                foreach (SearchMovie result in results.Results)
                {
                    search = result;
                    break;
                }
movie = client.GetMovieAsync(search.Id, MovieMethods.Images | MovieMethods.Videos).Result;

foreach (Video video in movie.Videos.Results)
{
    Console.WriteLine($"Trailer: {video.Type} ({video.Site}), {video.Name}");
    Console.WriteLine(video.Key);
    youtubeLink = "https://www.youtube.com/watch?v=" + video.Key;
    break;
}
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "INSERT INTO movie_trailer (movie_title, movie_trailer_link) VALUES ('" + id + "','" + youtubeLink + "')";
                    cmd.Connection = conn;
                    conn.Open();
                    try
                    {
                        int aff = cmd.ExecuteNonQuery();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Added");
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return youtubeLink;
            }
            Console.WriteLine("Emply space added");
            return "";

            //foreach (Video video in movie.Videos.Results) {
            //    Console.WriteLine($"Trailer: {video.Type} ({video.Site}), {video.Name}");
            //    Console.WriteLine(video.Key);
            //    youtubeLink = "https://www.youtube.com/watch?v=" + video.Key;
            //    break;
            //        }
            //return youtubeLink;
        }

      

        public static String getLinkByName(String name)
        {

          


            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
            conn.Open();
            NpgsqlTransaction tran = conn.BeginTransaction();

            name = String.Join("", name.Split("'"));
            Console.WriteLine(name);
            NpgsqlCommand command = new NpgsqlCommand("SELECT movie_trailer_link FROM movie_trailer WHERE movie_title = '" + name + "'", conn);

            // Execute the query and obtain a result set
           
            
            NpgsqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                if (dr[0] != null)
                {
                    try
                    {
                        Console.WriteLine("returned with db");
                        return (string)dr[0];
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }


                }

            }
            else
            {
                conn.Close();
                TMDbClient client = new TMDbClient("82bb3b5cf7b870891c07d7d362fe888a");
                SearchContainer<SearchMovie> results = client.SearchMovieAsync(name).Result;

                String youtubeLink = "";
                SearchMovie search = new SearchMovie();
                foreach (SearchMovie result in results.Results)
                {
                    search = result;
                    break;
                }
                Movie movie = client.GetMovieAsync(search.Id, MovieMethods.Images | MovieMethods.Videos).Result;

                try
                {
                    foreach (Video video in movie.Videos.Results)
                    {
                        Console.WriteLine($"Trailer: {video.Type} ({video.Site}), {video.Name}");
                        Console.WriteLine(video.Key);
                        youtubeLink = "https://www.youtube.com/watch?v=" + video.Key;
                        break;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }



                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "INSERT INTO movie_trailer (movie_title, movie_trailer_link) VALUES ('" + name + "','" + youtubeLink + "')";
                    cmd.Connection = conn;
                    conn.Open();
                    try
                    {
                        int aff = cmd.ExecuteNonQuery();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Added");
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return youtubeLink;
            }
            Console.WriteLine("Emply space added");
            return "";


        }

    }

  

}
