using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jokes.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Jokes.Web.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private string _connectionString;
  

        public HomeController(IConfiguration configurtation)
        {
            _connectionString = configurtation.GetConnectionString("ConStr");
        }
        [Route("getjoke")]
        [HttpGet]
        public Joke GetJoke()
        {
            var repo = new JokeRepository(_connectionString);

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var client = new HttpClient(handler);
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/programming/random").Result;

            var joke = JsonConvert.DeserializeObject<List<Joke>>(json).First();
            var originalJoke = repo.AddOrGetJoke(joke);
            return originalJoke;

       

        }
        [Route("viewall")]
        [HttpGet]
        public List<Joke> ViewAll()
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetAllJokes();
        }
        [Route("like")]
        [HttpPost]
        public DateTime Like(Joke joke)
        {
            var userRepo = new UserRepository(_connectionString);
            var jokeRepo = new JokeRepository(_connectionString);
            string email = User.FindFirst("user")?.Value;
            var userId= userRepo.GetByEmail(email).Id;
            return jokeRepo.LikeJoke(userId, joke.Id);
        }
        [Route("dislike")]
        [HttpPost]
        public DateTime Dislike(Joke joke)
        {
            var userRepo = new UserRepository(_connectionString);
            var jokeRepo = new JokeRepository(_connectionString);
            string email = User.FindFirst("user")?.Value;
            var userId = userRepo.GetByEmail(email).Id;
           return jokeRepo.DislikeJoke(userId, joke.Id);
        }
        [Route("getlikesanddislikes")]
        [HttpGet]
        public LikesCounts GetLikesAndDislikes(int jokeId)
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetLikes(jokeId);
        }
      
        [Route("getuserlikedjoke")]
        [HttpGet]
        public UserLikedJokes GetUserLikedJoke(int jokeId)
        {
          
            var userRepo = new UserRepository(_connectionString);
            var jokeRepo = new JokeRepository(_connectionString);
            string email = User.FindFirst("user")?.Value;
            var userId = userRepo.GetByEmail(email).Id;
            return jokeRepo.UserLikedJoke(userId, jokeId);

        }
        [Route("likedordisliked")]
        [HttpGet]
        public bool LikedOrDisliked(int jokeId)
        {
            var userRepo = new UserRepository(_connectionString);
            var jokeRepo = new JokeRepository(_connectionString);
            string email = User.FindFirst("user")?.Value;
            var userId = userRepo.GetByEmail(email).Id;
            return jokeRepo.LikedOrDisliked(userId, jokeId);

        }


    }
}
