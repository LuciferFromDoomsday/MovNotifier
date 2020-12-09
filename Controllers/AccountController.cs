using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovNotifier.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovNotifier.Controllers
{


   


    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
        MovieContext _context = new MovieContext();
        //private readonly UserManager<User> userManager;
        //private readonly SignInManager<User> signInManager;
        public AccountController() { }


        public IActionResult GoogleLogout()
        {

            CurrentUser.setCurrentUser(null);

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }



            //ClearCookies();

            //return RedirectToAction("Registration" , "Home");
            return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:5001" + Url.Action("Registration", "Home" )); 

        }

        // GET: /<controller>/
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties,GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                }
                );

            foreach(var obj in claims.ToArray()) {
                Console.WriteLine(obj.Value);
            }

           
                User user = new User();
                user.Email = claims.ToArray().Last().Value;

            user.Name = claims.ToArray()[1].Value;

            user.Password = "PASSWORD";
                if (user.Email == "ayannaimankhan@gmail.com")
                {
                    user.Role = "ADMIN";

                }
                else
                {
                    user.Role = "USER";
                }
            // var res = await userManager.CreateAsync(user);
            try
            {
                User d = _context.Users.Where(s => s.Email == user.Email).First();
                CurrentUser.setCurrentUser(d);
                return RedirectToAction("Index", "Home");
              
            }
            catch {
               
                if (user.Email != null)
                {

                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                CurrentUser.setCurrentUser(user);
                return RedirectToAction("Index", "Home");
            }
             

      

            
            //catch
            //{ 

            //    return RedirectToAction("Registration", "Home");
            //}

        }
        
    }
}
