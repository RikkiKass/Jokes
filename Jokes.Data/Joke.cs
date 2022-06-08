using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jokes.Data
{
    public class Joke
    {
        public int Id { get; set; }

        public int OriginalId { get; set; }
        public string Setup { get; set; }

        public string Punchline { get; set; }
        public List<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
