using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokes.Data
{
    public class UserRepository
    {
        private string _conn;
        public UserRepository(string connectionString)
        {
            _conn = connectionString;
        }
        public void Signup(User user, string password)
        {
            using var context = new JokeDataContext(_conn);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = passwordHash;
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(String email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid)
            {
                return null;
            }
            return user;

        }
        public User GetByEmail(string email)
        {
            using var context = new JokeDataContext(_conn);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
