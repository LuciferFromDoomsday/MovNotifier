using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MovNotifier.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ListRecommendations> ListRecommendations { get; set; }

        public DbSet<ListWatchedMovies> ListWatchedMovies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<ListActors> ListActors { get; set; }

        public DbSet<ListLanguages> ListLanguages { get; set; }

        public DbSet<ListGenres> ListGenres { get; set; }

      


        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {
        }

        public MovieContext()
        {
            
            Database.Migrate();
            Database.EnsureCreated();
           

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<Genre>(entity =>
            //{
            //    entity.ToTable("Genres");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .HasColumnType("int(11)");

       

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasColumnName("name")
            //        .HasMaxLength(255)
            //        .IsUnicode(false);
            //});





            modelBuilder.Entity<Movie>()
    .HasKey(p => p.Id);

            modelBuilder.Entity<ListGenres>().HasKey(sc => new { sc.MovieId, sc.GenreId });

            modelBuilder.Entity<ListGenres>()
                .HasOne<Movie>(sc => sc.Movie)
                .WithMany(s => s.Genres)
                .HasForeignKey(sc => sc.GenreId);


            modelBuilder.Entity<ListGenres>()
                .HasOne<Genre>(sc => sc.Genre)
                .WithMany(s => s.Movies)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<ListActors>().HasKey(sc => new { sc.MovieId, sc.ActorId });

            modelBuilder.Entity<ListActors>()
                .HasOne<Movie>(sc => sc.Movie)
                .WithMany(s => s.Actors)
                .HasForeignKey(sc => sc.ActorId);


            modelBuilder.Entity<ListActors>()
                .HasOne<Actor>(sc => sc.Actor)
                .WithMany(s => s.Movies)
                .HasForeignKey(sc => sc.MovieId);


            modelBuilder.Entity<ListLanguages>().HasKey(sc => new { sc.MovieId, sc.LanguageId });

            modelBuilder.Entity<ListLanguages>()
                .HasOne<Movie>(sc => sc.Movie)
                .WithMany(s => s.Languages)
                .HasForeignKey(sc => sc.LanguageId);


            modelBuilder.Entity<ListLanguages>()
                .HasOne<Language>(sc => sc.Language)
                .WithMany(s => s.Movies)
                .HasForeignKey(sc => sc.MovieId);



            modelBuilder.Entity<ListWatchedMovies>().HasKey(sc => new { sc.UserId, sc.MovieId });

            modelBuilder.Entity<ListRecommendations>().HasKey(sc => new { sc.UserId, sc.MovieId });

            modelBuilder.Entity<ListRecommendations>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.Movies)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<ListRecommendations>()
                .HasOne<Movie>(sc => sc.Movie)
                .WithMany(s => s.Users)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<ListWatchedMovies>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.WatchedMovies)
                .HasForeignKey(sc => sc.MovieId);

            base.OnModelCreating(modelBuilder);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
            base.OnConfiguring(optionsBuilder);
        }


    }

}

