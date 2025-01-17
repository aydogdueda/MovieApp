using filmsitesi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace filmsitesi.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LogoutController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;       
        }

   
        [HttpPost]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
    
            await _signInManager.SignOutAsync();

        
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            
           
            return Redirect("/");
        }
    }
}
