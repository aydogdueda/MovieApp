using filmsitesi.Dtos;
using filmsitesi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace filmsitesi.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginController> _logger;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<LoginController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
     
                return View(loginDto);
            }


            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, true);
            if (result.Succeeded)
            {
        
                var user = await _userManager.FindByNameAsync(loginDto.Username);
                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.UserName);
                }

       
                return Redirect("/");
            }

         
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(loginDto);
        }
    }
}
