using System.Net;
using System.Text.RegularExpressions;
using CarShop.Services;
using CarShop.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class UsersController:Controller

    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register( RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
            return    this.Redirect("/");
            }
            if (model.Username.Length>20||4>model.Username.Length||string.IsNullOrWhiteSpace(model.Username))
            {
                return this.Error("Username must be with  min length 4 and max length 20");
            }
            Regex rgx = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (string.IsNullOrWhiteSpace(model.Email) || !rgx.IsMatch(model.Email)) 
            {
                return this.Error("Email must be valid");
            }
            if (model.Password!= model.ConfirmPassword)
            {
                return this.Error("Password and confirm Password must be equal");
            }
            if (model.Password.Length>20||model.Password.Length<5)
            {
                return this.Error("Password must be with  min length 5 and max length 20");
            }
            if (!usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("Username is not valid");
            }
            usersService.Create(model.Username, model.Email, model.Password, model.UserType);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(model.Username,model.Password);

            if (userId == null)
            {
                return this.Error("Invalid username/password");
            }

            this.SignIn(userId);

            return this.View("/Cars/All");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged-in users can log-out.");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}