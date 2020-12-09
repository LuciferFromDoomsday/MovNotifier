//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;

//namespace MovNotifier.Models
//{
//    public class UserDbContext : IdentityDbContext<User>
//    {
        

//        public DbSet<User> AppUsers { get; set; }

//        public DbSet<ListRecommendations> ListRecommendations { get; set; }
//        public DbSet<ListWatchedMovies> ListWatchedMovies { get; set; }


//        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
//        {
//        }

//        public UserDbContext()
//        {
//            Database.Migrate();
//            Database.EnsureCreated();
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {


//            modelBuilder.Entity<ListWatchedMovies>().HasKey(sc => new { sc.UserId, sc.MovieId });

//            modelBuilder.Entity<ListRecommendations>().HasKey(sc => new { sc.UserId, sc.MovieId });

//            modelBuilder.Entity<ListRecommendations>()
//                .HasOne<User>(sc => sc.User)
//                .WithMany(s => s.Movies)
//                .HasForeignKey(sc => sc.MovieId);

//            modelBuilder.Entity<ListWatchedMovies>()
//                .HasOne<User>(sc => sc.User)
//                .WithMany(s => s.WatchedMovies)
//                .HasForeignKey(sc => sc.MovieId);

           
          



//        }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseNpgsql("Host=localhost;Database=MovieDB;Username=postgres;Password=123456789");
//        }


//    }

//}

