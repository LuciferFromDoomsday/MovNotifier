using System;
using System.Linq;
using System.Threading.Tasks;
using MovNotifier.Models;

namespace MovNotifier.Services
{
    public class GenreServiceImpl : GenreService
    {
        public GenreServiceImpl()
        {
        }

        private MovieContext _context = new MovieContext();
        public async Task<bool> IsGenreExist(string Name)
        {

            await Task.Delay(1000);

            if (_context.Genres.Where(s => s.Name == Name).First() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
