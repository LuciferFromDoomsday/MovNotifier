using System;
using System.Threading.Tasks;

namespace MovNotifier.Services
{
    public interface GenreService
    {
        Task<bool> IsGenreExist(String Name);
    }
}
