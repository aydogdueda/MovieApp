using filmsitesi.Dtos;
using filmsitesi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace filmsitesi.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
    
                var existingUser = await _userManager.FindByEmailAsync(appUserRegisterDto.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(appUserRegisterDto); 
                }


                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Name,
                    Email = appUserRegisterDto.Email,
                };

    
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

                if (result.Succeeded)
                {
                
                    return Redirect("/login");  
                }
                else
                {
                  
                    foreach (var error in result.Errors)
                    {
                        var errorMessage = GetTurkishErrorMessage(error.Code);
                        ModelState.AddModelError("", errorMessage);
                    }
                }
            }

            
            return View(appUserRegisterDto);
        }

        private string GetTurkishErrorMessage(string errorCode)
        {
            switch (errorCode)
            {
                case "DuplicateEmail":
                    return "Bu e-posta adresi zaten kullanılıyor.";
                case "PasswordTooShort":
                    return "Şifreniz çok kısa. En az 6 karakter olmalıdır.";
                case "PasswordRequiresUpper":
                    return "Şifrenizde en az bir büyük harf olmalıdır.";
                case "PasswordRequiresLower":
                    return "Şifrenizde en az bir küçük harf olmalıdır.";
                case "PasswordRequiresDigit":
                    return "Şifrenizde en az bir rakam olmalıdır.";
                case "PasswordRequiresNonAlphanumeric":
                    return "Şifrenizde en az bir özel karakter olmalıdır.";
                default:
                    return "Bir hata oluştu: " + errorCode;
            }
        }
    }
}
