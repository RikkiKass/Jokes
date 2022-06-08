using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokes.Data
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public DateTime DateLiked { get; set; }
        public bool Liked { get; set; }
    }
}
