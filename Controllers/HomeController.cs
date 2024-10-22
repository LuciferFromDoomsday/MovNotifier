﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovNotifier.Models;
using MovNotifier.Services;
using OMDbApiNet.Model;
using MailKit.Net.Smtp;
using MimeKit;
using System.Web;
using System.Text.RegularExpressions;
using Npgsql;
using System.Data;

namespace MovNotifier.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        MovieContext _context = new MovieContext();

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        public ActionResult Index()
        {

            UpcomingMovies upcoming = new UpcomingMovies();

            dynamic myModel = new ExpandoObject();
            RecommendationService recommendationService = new RecommendationService();
            myModel.Recommendation = recommendationService.GetRecommendations();
            myModel.MailRecommendation = recommendationService.GetRecommendations().First().Id;
            myModel.favoriteDirectors = recommendationService.getFavoriteDirectors();
            myModel.favoriteActors = recommendationService.getFavoriteActors();
            myModel.favoriteGenres = recommendationService.getFavoriteGenres();
            myModel.historyCount = recommendationService.GetMoviesOfUser().Count();
            myModel.UpcomingMovies = upcoming.getUpcomingMovies().Results;
            recommendationService.GetMoviesOfUser().Reverse();
            if (recommendationService.GetMoviesOfUser().Count() < 6)
            {

                myModel.history = recommendationService.GetMoviesOfUser();
            }
            else
            {
                myModel.history = recommendationService.GetMoviesOfUser().Take(5);
            }
            return View(myModel);

        }
        public ActionResult SearchResult(string search)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                try
                {
                    if (search == null)
                    {
                        MovieAPI api = new MovieAPI();

                        dynamic myModel = new ExpandoObject();
                        myModel.Movies = api.GetMoviesByYear("2020");
                        return View(myModel);
                    }
                    else
                    {
                        MovieAPI api = new MovieAPI();

                        dynamic myModel = new ExpandoObject();
                        myModel.Movies = api.GetMoviesByTitle(search);
                        return View(myModel);

                    }

                }
                catch
                {
                    dynamic myModel = new ExpandoObject();
                    myModel.Movies = new List<SearchItem>();
                    return View(myModel);
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }

            }


        [HttpGet]
        public ActionResult ExistingMovieDetails(string Id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {

                Movie m = _context.Movies.Where(s => s.Id == int.Parse(Id)).First();
                Console.WriteLine(m.Title);
                dynamic myModel = new ExpandoObject();
                myModel.Movie = m;

                List<Genre> genres = new List<Genre>();
                List<Actor> actors = new List<Actor>();
                if (_context.ListGenres.Where(s => s.GenreId == m.Id) != null)
                {
                    foreach (ListGenres genre in _context.ListGenres.Where(s => s.GenreId == m.Id).ToList())
                    {

                        // Console.WriteLine(genre.MovieId);
                        try
                        {

                            genres.Add(_context.Genres.Where(s => s.Id == genre.MovieId).First());
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
                myModel.Genres = genres.Take(4).ToList();

                if (_context.ListActors.Where(s => s.ActorId == m.Id) != null)
                {

                    foreach (ListActors genre in _context.ListActors.Where(s => s.ActorId == m.Id).ToList())
                    {

                        Console.WriteLine(genre.MovieId);

                        try
                        {

                            Actor actor = _context.Actors.Where(s => s.Id == genre.MovieId).First();
                            actors.Add(actor);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                myModel.Actors = actors;
                NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
                conn.Open();
                NpgsqlTransaction tran = conn.BeginTransaction();

                NpgsqlCommand command = new NpgsqlCommand("SELECT exists(SELECT 1 from user_history where user_id = " + CurrentUser.getCurrentUser().Id + " and movie_id = " + m.Id + ")", conn);

                // Execute the query and obtain a result set
                using (NpgsqlDataReader dr = command.ExecuteReader())
                {

                    if (dr.Read())
                    {
                        Console.Write("{0} \n", dr[0]);
                        if ((bool)dr[0] == false)
                        {
                            conn.Close();
                            conn.Open();
                            using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO user_history (user_id, movie_id) VALUES (" + CurrentUser.getCurrentUser().Id + "," + m.Id + " )", conn))
                            {
                                //  conn.Open();
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
                        }
                        else
                        {
                            Console.WriteLine("Not Added");
                        }

                    }
                }
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }


        }

        [HttpGet]
        public ActionResult Details(string IMDBID)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                MovieAPI api = new MovieAPI();

                Console.WriteLine(IMDBID);
                Item i = api.GetMovieById(IMDBID);

                try
                {
                    Movie m = _context.Movies.Where(s => s.Title == i.Title).First();
                    dynamic myModel = new ExpandoObject();
                    myModel.Movie = m;

                    List<Genre> genres = new List<Genre>();
                    List<Actor> actors = new List<Actor>();
                    if (_context.ListGenres.Where(s => s.GenreId == m.Id) != null)
                    {
                        foreach (ListGenres genre in _context.ListGenres.Where(s => s.GenreId == m.Id).ToList())
                        {

                            // Console.WriteLine(genre.MovieId);
                            try
                            {

                                genres.Add(_context.Genres.Where(s => s.Id == genre.MovieId).First());
                            }
                            catch
                            {
                                continue;
                            }

                        }
                    }
                    myModel.Genres = genres.Take(4).ToList();

                    if (_context.ListActors.Where(s => s.ActorId == m.Id) != null)
                    {

                        foreach (ListActors genre in _context.ListActors.Where(s => s.ActorId == m.Id).ToList())
                        {

                            Console.WriteLine(genre.MovieId);

                            try
                            {

                                Actor actor = _context.Actors.Where(s => s.Id == genre.MovieId).First();
                                actors.Add(actor);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    myModel.Actors = actors;



                    NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
                    conn.Open();
                    NpgsqlTransaction tran = conn.BeginTransaction();

                    NpgsqlCommand command = new NpgsqlCommand("SELECT exists(SELECT 1 from user_history where user_id = " + CurrentUser.getCurrentUser().Id + " and movie_id = " + m.Id + ")", conn);

                    // Execute the query and obtain a result set
                    NpgsqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        Console.Write("{0} \n", dr[0]);
                        if ((bool)dr[0] == false)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand();
                            cmd.CommandText = "INSERT INTO user_history (user_id, movie_id) VALUES (" + CurrentUser.getCurrentUser().Id + "," + m.Id + " )";
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
                        else
                        {
                            Console.WriteLine("Not Added");
                        }

                    }




                    return View(myModel);
                }
                catch
                {
                    Movie m = new Movie();

                    m.Title = i.Title;
                    m.Runtime = i.Runtime;
                    m.Poster = i.Poster;
                    m.Description = i.Plot + "\n" + i.Awards;
                    m.ReleasDate = i.Released;



                    try
                    {
                        if (i.ImdbRating != "N/A" || i.ImdbRating != "" || i.Metascore != "N/A" || i.Metascore != "" || i.ImdbRating != null || i.Metascore != null)
                        {
                            double imdb = double.Parse(i.ImdbRating);
                            Console.WriteLine(i.Metascore);
                            double metascore = double.Parse(Regex.Replace(i.Metascore, @"\s+", "")) / 10.0;

                            Console.WriteLine(imdb + " " + metascore + " ");

                            m.AvgRating = (imdb + metascore) / 2.0;
                        }
                    }
                    catch
                    {
                        m.AvgRating = 4;

                    }


                    Country c = new Country();
                    if (i.Country.Contains(","))
                    {
                        string[] countries = i.Country.Split(",");

                        c.name = countries.First();

                    }
                    else
                    {
                        c.name = i.Country;
                    }

                    try
                    {
                        Country country = _context.Countries.Where(s => s.name == c.name).First();
                        m.Country = country;

                    }
                    catch
                    {
                        _context.Countries.Add(c);
                        _context.SaveChanges();
                        m.Country = c;
                    }
                    Director director = new Director();
                    if (i.Director.Contains(","))
                    {
                        string[] directors = i.Director.Split(",");

                        director.name = directors.First();

                    }
                    else
                    {
                        director.name = i.Director;
                    }

                    try
                    {
                        Director director1 = _context.Directors.Where(s => s.name == director.name).First();
                        m.Directors = director1;

                    }
                    catch
                    {
                        _context.Directors.Add(director);
                        _context.SaveChanges();
                        m.Directors = director;
                    }

                    _context.Movies.Add(m);
                    _context.SaveChanges();

                    Movie NewM = _context.Movies.Where(s => s.Title == m.Title).First();

                    string[] languages = i.Language.Split(",");

                    foreach (string language in languages)
                    {
                        Language lang = new Language();
                        try
                        {
                            Language language1 = _context.Languages.Where(s => s.name == language).First();
                            Console.WriteLine(language1.name);
                            try
                            {
                                ListLanguages lsn = _context.ListLanguages.Where(s => s.LanguageId == NewM.Id).Where(s => s.MovieId == language1.Id).First();
                                Console.WriteLine(lsn.MovieId);

                            }
                            catch
                            {
                                ListLanguages ls = new ListLanguages();
                                ls.Language = language1;
                                ls.LanguageId = language1.Id;
                                ls.Movie = NewM;
                                ls.MovieId = NewM.Id;
                                _context.ListLanguages.Add(ls);
                                _context.SaveChanges();
                            }

                        }
                        catch
                        {
                            lang.name = language;
                            _context.Languages.Add(lang);
                            _context.SaveChanges();

                            ListLanguages ls = new ListLanguages();
                            ls.Language = _context.Languages.Where(s => s.name == lang.name).First();
                            ls.LanguageId = _context.Languages.Where(s => s.name == lang.name).First().Id;
                            ls.Movie = NewM;
                            ls.MovieId = NewM.Id;
                            _context.ListLanguages.Add(ls);
                            _context.SaveChanges();
                        }
                    }

                    string[] genres = i.Genre.Split(",");

                    foreach (string genre in genres)
                    {
                        Genre g = new Genre();
                        try
                        {
                            Genre genre1 = _context.Genres.Where(s => s.Name == genre).First();
                            try
                            {
                                ListGenres lsn = _context.ListGenres.Where(s => s.GenreId == NewM.Id).Where(s => s.MovieId == genre1.Id).First();

                            }
                            catch
                            {
                                ListGenres gs = new ListGenres();
                                gs.Genre = genre1;
                                gs.GenreId = genre1.Id;
                                gs.Movie = NewM;
                                gs.MovieId = NewM.Id;
                                _context.ListGenres.Add(gs);
                                _context.SaveChanges();
                            }
                        }
                        catch
                        {
                            g.Name = genre;
                            _context.Genres.Add(g);
                            _context.SaveChanges();

                            ListGenres ls = new ListGenres();
                            ls.Genre = _context.Genres.Where(s => s.Name == g.Name).First();
                            ls.GenreId = _context.Genres.Where(s => s.Name == g.Name).First().Id;
                            ls.Movie = NewM;
                            ls.MovieId = NewM.Id;
                            _context.ListGenres.Add(ls);
                            _context.SaveChanges();
                        }
                    }
                    string[] actors = i.Actors.Split(",");

                    foreach (string actor in actors)
                    {
                        Actor a = new Actor();
                        try
                        {
                            Actor actor1 = _context.Actors.Where(s => s.name == actor).First();
                            try
                            {
                                ListActors lsn = _context.ListActors.Where(s => s.ActorId == NewM.Id).Where(s => s.MovieId == actor1.Id).First();

                            }
                            catch
                            {
                                ListActors ac = new ListActors();
                                ac.Actor = actor1;
                                ac.ActorId = actor1.Id;
                                ac.Movie = NewM;
                                ac.MovieId = NewM.Id;
                                _context.ListActors.Add(ac);
                                _context.SaveChanges();
                            }
                        }
                        catch
                        {
                            a.name = actor;
                            _context.Actors.Add(a);
                            _context.SaveChanges();

                            ListActors ls = new ListActors();
                            ls.Actor = _context.Actors.Where(s => s.name == a.name).First();
                            ls.ActorId = NewM.Id;
                            ls.Movie = NewM;
                            ls.MovieId = _context.Actors.Where(s => s.name == a.name).First().Id;
                            _context.ListActors.Add(ls);
                            _context.SaveChanges();
                        }
                    }

                    //try
                    //{
                    //    ListRecommendations lr = _context.ListRecommendations.Where(s => s.MovieId == NewM.Id).Where(s => s.UserId == CurrentUser.getCurrentUser().Id).First();
                    //}
                    //catch
                    //{
                    //    ListRecommendations lr = new ListRecommendations
                    //    {
                    //        UserId = CurrentUser.getCurrentUser().Id,

                    //        MovieId = NewM.Id
                    //    };

                    //    _context.ListRecommendations.Add(lr);
                    //    _context.SaveChanges();
                    //}
                    m = NewM;
                    dynamic myModel = new ExpandoObject();
                    myModel.Movie = m;

                    List<Genre> genres1 = new List<Genre>();
                    List<Actor> actors1 = new List<Actor>();
                    if (_context.ListGenres.Where(s => s.GenreId == m.Id) != null)
                    {
                        foreach (ListGenres genre in _context.ListGenres.Where(s => s.GenreId == m.Id).ToList())
                        {

                            // Console.WriteLine(genre.MovieId);
                            try
                            {

                                genres1.Add(_context.Genres.Where(s => s.Id == genre.MovieId).First());
                            }
                            catch
                            {
                                continue;
                            }

                        }
                    }
                    myModel.Genres = genres1.Take(4).ToList();

                    if (_context.ListActors.Where(s => s.ActorId == m.Id) != null)
                    {

                        foreach (ListActors genre in _context.ListActors.Where(s => s.ActorId == m.Id).ToList())
                        {

                            Console.WriteLine(genre.MovieId);

                            try
                            {

                                Actor actor = _context.Actors.Where(s => s.Id == genre.MovieId).First();
                                actors1.Add(actor);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    myModel.Actors = actors1;

                    NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.CommandText = "INSERT INTO user_history (user_id, movie_id) VALUES (" + CurrentUser.getCurrentUser().Id + "," + m.Id + " )";
                    cmd.Connection = conn;
                    conn.Open();
                    try
                    {
                        int aff = cmd.ExecuteNonQuery();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return View(myModel);
                }

            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }



        }

        [HttpGet]
        public IActionResult SearchMovie(string term)
        {

            MovieAPI api = new MovieAPI();



            var result = (from N in api.GetMoviesByTitle(term)
                          where N.Title.Contains(term)
                          select new { value = N.Title });

            return Json(result);
        }

        public ActionResult Registration()
        {
            if (CurrentUser.getCurrentUser() == null)
            {
                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }

        }
        //public ActionResult LogIn()
        //{
        //    return View();
        //}

        public ActionResult Page403()
        {
            return View();
        }





        public ActionResult Admin()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
                    dynamic myModel = new ExpandoObject();
                    myModel.Movies = _context.Movies.ToList();
                    myModel.Genres = _context.Genres.ToList();
                    myModel.Actors = _context.Actors.ToList();
                    myModel.Languages = _context.Languages.ToList();
                    myModel.Directors = _context.Directors.ToList();
                    myModel.Countries = _context.Countries.ToList();
                    myModel.ErrorMessage = errMsg;


                    ; return View(myModel);
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateMovie(Movie movie , String Directors , String Country)
        {

            if (ModelState.IsValid)
            {

                Director d = _context.Directors.Where(s => s.Id == int.Parse(Directors)).First();

                movie.Directors = d;

                Country c = _context.Countries.Where(s => s.ID == int.Parse(Country)).First();

                movie.Country = c;


                _context.Movies.Add(movie);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your movie didn't pass the validation!!";
            }
            return RedirectToAction("Admin", "Home");

        }
        public ActionResult UpdateMoviePage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    Movie m = _context.Movies.Where(s => s.Id == id).First();



                    dynamic myModel = new ExpandoObject();
                    myModel.Id = id;
                    myModel.CurrentMovie = m;
                    myModel.Movies = _context.Movies.ToList();
                    myModel.Genres = _context.Genres.ToList();
                    myModel.Actors = _context.Actors.ToList();

                    myModel.Languages = _context.Languages.ToList();

                    List<Director> directors = _context.Directors.ToList();
                    if (m.Directors != null)
                    {
                        directors.Remove(m.Directors);
                        directors.Insert(0, m.Directors);
                    }
                    myModel.Directors = directors;


                    List<Country> countries = _context.Countries.ToList();
                    if (m.Country != null)
                    {
                        countries.Remove(m.Country);
                        countries.Insert(0, m.Country);
                    }
                    myModel.Countries = countries;

                    return View(myModel);
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
        }
            else
            {
                return RedirectToAction("Registration", "Home");
    }
}


        [HttpPost]
        public ActionResult UpdateMovie(Movie movie , String Directors, String Country , String Id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                Movie m = _context.Movies.Where(s => s.Id == Int32.Parse(Id)).First();
                m.Title = movie.Title;
                m.Runtime = movie.Runtime;
                m.ReleasDate = movie.ReleasDate;
                m.Poster = movie.Poster;
                m.AvgRating = movie.AvgRating;
                m.Description = movie.Description;
                m.Directors = _context.Directors.Where(s => s.Id == int.Parse(Directors)).First();
                m.Country = _context.Countries.Where(s => s.ID == int.Parse(Country)).First(); ;

                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated ! Movie didn't pass the validation!!";
            }
            

          
            return RedirectToAction("Admin", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }


        }





        [HttpPost]
        public bool DeleteMovie(int id)
        {

            try
            {
                Movie movie = _context.Movies.Where(s => s.Id == id).First();
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }


        public ActionResult Genres()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
            dynamic myModel = new ExpandoObject();
            myModel.Genres = _context.Genres.ToList();
            myModel.ErrorMessage = errMsg;
            return View(myModel);
        }
                else
                {
                    return RedirectToAction("Page403", "Home");
    }
}
            else
{
    return RedirectToAction("Registration", "Home");
}
  
        }
        public ActionResult Language()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
            dynamic myModel = new ExpandoObject();
            myModel.Languages = _context.Languages.ToList();
            myModel.ErrorMessage = errMsg;
            return View(myModel);
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }
         

        [HttpPost]
        public ActionResult CreateLanguage(Language language)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                _context.Languages.Add(language);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your Language didn't pass the validation!!";
            }

            return RedirectToAction("Language", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }



        public ActionResult UpdateLanguagePage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    return View(_context.Languages.Where(s => s.Id == id).First());
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateLanguage(Language language)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                Language d = _context.Languages.Where(s => s.Id == language.Id).First();
                d.name = language.name;

                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated ! Language didn't pass the validation!!";
            }
         
            return RedirectToAction("Language", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public bool DeleteLanguage(int id)
        {
            try
            {
                Language language = _context.Languages.Where(s => s.Id == id).First();
                _context.Languages.Remove(language);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }




        [HttpPost]
        public ActionResult CreateGenre(Genre genre)
        {

            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                ViewData["Message"] = "Success !";
                _context.Genres.Add(genre);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your Genre didn't pass the validation!!";
            }
            return RedirectToAction("Genres", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }


        public ActionResult UpdateGenrePage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    Console.WriteLine(_context.Genres.Where(s => s.Id == id).First().Name);
            return View(_context.Genres.Where(s => s.Id == id).First());
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateGenre(Genre genre)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {

                    if (ModelState.IsValid)
                    {
                        Genre d = _context.Genres.Where(s => s.Id == genre.Id).First();
                        d.Name = genre.Name;

                        _context.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Not Updated ! Genre didn't pass the validation!!";
                    }

                    return RedirectToAction("Genres", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
                }
                else
                {
                    return RedirectToAction("Registration", "Home");
                }
            }

        [HttpPost]
        public bool DeleteGenre(int id)
        {
            try
            {
                Genre genre = _context.Genres.Where(s => s.Id == id).First();
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public ActionResult Countries()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
            dynamic myModel = new ExpandoObject();
            myModel.Countries = _context.Countries.ToList();
            myModel.ErrorMessage = errMsg;
            return View(myModel);
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }


        }
    


        [HttpPost]
        public ActionResult CreateCountry(Country country)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                _context.Countries.Add(country);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your Country didn't pass the validation!!";
            }
          
            return RedirectToAction("Countries", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }


        }



        public ActionResult UpdateCountryPage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    return View(_context.Countries.Where(s => s.ID == id).First());
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateCountry(Country country)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
                    {
                        Country d = _context.Countries.Where(s => s.ID == country.ID).First();
                        d.name = country.name;

                        _context.SaveChanges();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Not Updated ! Genre didn't pass the validation!!";
                    }


                    return RedirectToAction("Countries", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public bool DeleteCountry(int id)
        {
            try
            {
                Country country = _context.Countries.Where(s => s.ID == id).First();
                _context.Countries.Remove(country);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }



        public ActionResult Actors()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
            dynamic myModel = new ExpandoObject();
            myModel.Actors = _context.Actors.ToList();
            myModel.ErrorMessage = errMsg;
            return View(myModel);
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }



        [HttpPost]
        public ActionResult CreateActor(Actor actor)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                _context.Actors.Add(actor);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your Actor didn't pass the validation!!";
            }

      
            return RedirectToAction("Actors", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }



        public ActionResult UpdateActorPage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {

                    return View(_context.Actors.Where(s => s.Id == id).First());
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateActor(Actor actor)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                Actor d = _context.Actors.Where(s => s.Id == actor.Id).First();
                d.name = actor.name;

                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated ! Genre didn't pass the validation!!";
            }
         
            return RedirectToAction("Actors", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public bool DeleteActor(int id)
        {

            try
            {
                Actor actor = _context.Actors.Where(s => s.Id == id).First();
                _context.Actors.Remove(actor);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public ActionResult Directors()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    var errMsg = TempData["ErrorMessage"] as string;
            dynamic myModel = new ExpandoObject();
            myModel.Directors = _context.Directors.ToList();
            myModel.ErrorMessage = errMsg;
            return View(myModel);
        }
                else
                {
                    return RedirectToAction("Page403", "Home");
    }
}
            else
{
    return RedirectToAction("Registration", "Home");
}
            
        }

        public ActionResult WatchList()
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                RecommendationService rs = new RecommendationService();

            dynamic myModel = new ExpandoObject();
              
            myModel.Movies = rs.GetMoviesOfUser().ToList();

         
             return View(myModel);
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }

        }




        [HttpPost]
        public ActionResult CreateDirector(Director director)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                _context.Directors.Add(director);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Your Actor didn't pass the validation!!";
            }

           
            return RedirectToAction("Directors", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }

        }



        public ActionResult UpdateDirectorPage(int id)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    return View(_context.Directors.Where(s => s.Id == id).First());
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateDirector(Director director)
        {
            if (CurrentUser.getCurrentUser() != null)
            {
                if (CurrentUser.getCurrentUser().Role == "ADMIN")
                {
                    if (ModelState.IsValid)
            {
                Director d = _context.Directors.Where(s => s.Id == director.Id).First();
                d.name = director.name;

                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated ! Genre didn't pass the validation!!";
            }
           
            return RedirectToAction("Directors", "Home");
                }
                else
                {
                    return RedirectToAction("Page403", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registration", "Home");
            }
        }

        [HttpPost]
        public bool DeleteDirector(int id)
        {
            try
            {
                Director director = _context.Directors.Where(s => s.Id == id).First();
                _context.Directors.Remove(director);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }


        [HttpPost]
        public bool DeleteHistoryMovie(int id)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "DELETE FROM user_history where movie_id = " + id;
                    cmd.Connection = conn;
                    conn.Open();
                    try
                    {
                        int aff = cmd.ExecuteNonQuery();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Deleted");
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        [HttpPost]
        public bool DeleteWholeHistory(int id)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "DELETE FROM user_history where user_id = " + id;
                    cmd.Connection = conn;
                    conn.Open();
                    try
                    {
                        int aff = cmd.ExecuteNonQuery();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Deleted");
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        [HttpPost]
        public ActionResult SendRecommendation(int Id)
        {
            Movie m = _context.Movies.Where(s => s.Id == Id).First();
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("MovNotifier",
            "movNotifier@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(CurrentUser.getCurrentUser().Name,
            CurrentUser.getCurrentUser().Email);
            message.To.Add(to);

            message.Subject = "This is Daily Movie recommendation which makes your evening more interesting with a good movie))";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Today's movie is : "+m.Title + " </h1>" +
                @" <img  src="" "+m.Poster +
                @"""  />"+
                "<h3>You can see the details by moving to this link : https://localhost:5001/Home/ExistingMovieDetails?Id=" + Id + "</h3>";
            bodyBuilder.TextBody = "You can see the details by moving to this link : https://localhost:5001/Home/ExistingMovieDetails?Id=" + Id;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("ayannaimankhan@gmail.com", "Superpassword");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return RedirectToAction("Index", "Home");
        }












    }






}
