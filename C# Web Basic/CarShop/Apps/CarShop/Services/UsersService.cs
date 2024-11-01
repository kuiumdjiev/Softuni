﻿using CarShop.Data;

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CarShop.Data.Models;

namespace CarShop.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string username, string email, string password, string userType)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password),
                IsMechanic = userType == "Mechanic"? true:false
            };
            dbContext.Add(user);
            dbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            return dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == ComputeHash(password)).Id;
        }

        public bool IsUserMechanic(string Userid)
        {
            return dbContext.Users.FirstOrDefault(x => x.Id == Userid).IsMechanic;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !dbContext.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
