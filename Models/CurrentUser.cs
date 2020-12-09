using System;
namespace MovNotifier.Models
{
    public class CurrentUser
    {
       public static User user;
       public CurrentUser()
        {
        }


        public static User getCurrentUser()
        {
            return CurrentUser.user;
        }
        public static void setCurrentUser(User user)
        {
          CurrentUser.user = user;
        }
    }
}
