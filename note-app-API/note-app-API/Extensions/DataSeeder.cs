using note_app_API.Database;
using note_app_API.Database.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace note_app_API.Extensions
{
    public class DataSeeder
    {
        public static void SeedData(DataContext context)
        {
            if(context.Users.Any()) { return; }
            var usersJSON = File.ReadAllText("Database/Seeding/users.json");
            var users = JsonSerializer.Deserialize<List<User>>(usersJSON);
            if(users is null) { return; }
            var passwordBytes = Encoding.UTF8.GetBytes("Password$");
            foreach (var user in users)
            {
                using var hashFunc = new HMACSHA256();
                user.PasswordHash = hashFunc.ComputeHash(passwordBytes);
                user.PasswordSalt = hashFunc.Key;
            }
            context.Users.AddRange(users);
            context.SaveChanges();

        }
    }
}
