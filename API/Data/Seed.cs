using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            //if we have any data context
            if (await context.Users.AnyAsync()) return;
            //if we don't have any data context
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            /*if any property we are creting in the Json file it wasn't in the correct way
            then we write this line of code to ensure every thing is works.*/
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach ( var user in users )
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();

                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                //we just adding it to entity framework traking
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
