using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokes.Data
{
    public class JokeRepository
    {
        private string _conn;
        public JokeRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public Joke AddOrGetJoke(Joke joke)
        {
            using var context = new JokeDataContext(_conn);
            
            var originalJoke = context.Jokes.FirstOrDefault(j => j.OriginalId == joke.Id);
            if (originalJoke == null)
            {
                var newJoke = new Joke
                {
                    OriginalId = joke.Id,
                    Punchline = joke.Punchline,
                    Setup = joke.Setup

                };
                context.Jokes.Add(newJoke);
                context.SaveChanges();
                return newJoke;

            }
            return originalJoke;

        }
        public LikesCounts GetLikes(int jokeId)
        {
            using var context = new JokeDataContext(_conn);
            int likes = context.UserLikedJokes.Where(j=>j.Liked==true && j.JokeId==jokeId).Count();
            int dislikes = context.UserLikedJokes.Where(j => j.Liked == false && j.JokeId == jokeId).Count();
            return  new LikesCounts
            {
                Likes = likes,
                Dislikes = dislikes
            };
        }
        
        public DateTime LikeJoke(int userId, int jokeId)
        {
            using var context = new JokeDataContext(_conn);
            var ulj = context.UserLikedJokes.FirstOrDefault(j => j.UserId == userId && j.JokeId == jokeId);
            if (ulj == null)
            {
                UserLikedJokes u = new UserLikedJokes
                {
                    UserId = userId,
                    JokeId = jokeId,
                    DateLiked = DateTime.Now,
                    Liked = true
                };
                context.UserLikedJokes.Add(u); 
                context.SaveChanges();

                return u.DateLiked.AddMinutes(1);

            }
            else
            {
                ulj.Liked = true;
                context.Update(ulj);
                context.SaveChanges();
                return ulj.DateLiked.AddMinutes(1);
            }

           
        }
        public DateTime DislikeJoke(int userId, int jokeId)
        {
            using var context = new JokeDataContext(_conn);
            
            var ulj = context.UserLikedJokes.FirstOrDefault(j => j.UserId == userId && j.JokeId == jokeId);
            if (ulj == null)
            {
                UserLikedJokes u = new UserLikedJokes
                {
                    UserId = userId,
                    JokeId = jokeId,
                    DateLiked = DateTime.Now,
                    Liked = false
                };
                context.UserLikedJokes.Add(u); 
                context.SaveChanges();
                return u.DateLiked.AddMinutes(1);
            }
            else
            {
                ulj.Liked = false;
                context.Update(ulj);
                context.SaveChanges();
                return ulj.DateLiked.AddMinutes(1);
            }

           

        }
        public List<Joke> GetAllJokes()
        {
            using var context = new JokeDataContext(_conn);
            return context.Jokes.ToList();

        }
        public UserLikedJokes UserLikedJoke(int userId, int jokeId)
        {
            using var context = new JokeDataContext(_conn);
            return context.UserLikedJokes.FirstOrDefault(ulj => ulj.JokeId == jokeId && ulj.UserId == userId);
            
        }
        public bool LikedOrDisliked(int userId, int jokeId)
        {
            using var context = new JokeDataContext(_conn);
            return context.UserLikedJokes.Any(ulj => ulj.JokeId == jokeId && ulj.UserId == userId);

        }
    }
}
