using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace filmsitesi.Controllers
{
    public class FilmController : Controller
    {
        private readonly HttpClient _httpClient;

        public FilmController()
        {
            _httpClient = new HttpClient();
        }


        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://api.themoviedb.org/3/movie/popular?api_key=7eb074f50b878696447656ca09abf573&language=en-US&page=1";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<FilmViewModel>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString);

            var filmList = apiResponse?.Results ?? new List<FilmViewModel>();

            var userName = User.Identity.IsAuthenticated ? User.Identity.Name : null;
            ViewData["UserName"] = userName;
            return View(filmList);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

        
            var film = await GetFilmById(id);

            if (film == null)
            {
                return NotFound();
            }

    
            return View(film);
        }

        private async Task<FilmViewModel> GetFilmById(int id)
        {
            var apiKey = "7eb074f50b878696447656ca09abf573"; // API anahtarınız
            var url = $"https://api.themoviedb.org/3/movie/{id}?api_key={apiKey}&language=en-US";

            var response = await _httpClient.GetStringAsync(url);
            var film = JsonSerializer.Deserialize<FilmViewModel>(response);

            return film;
        }



    }


    public class ApiResponse
    {
        [JsonPropertyName("results")]
        public List<FilmViewModel> Results { get; set; }
    }


    public class FilmViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        public string PosterUrl => "https://image.tmdb.org/t/p/w500" + PosterPath; 
    }
}
