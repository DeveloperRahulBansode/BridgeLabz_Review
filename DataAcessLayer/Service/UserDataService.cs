using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Context;
using DataAcessLayer.Entity;
using DataAcessLayer.Interface;
using DataAcessLayer.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Service
{
    public class UserDataService : IUserDataService
    {
        public readonly UserDbContext _userDbContext;

        public UserDataService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task AddUser(User user)
        {
            var isExist = await _userDbContext.Users.AnyAsync(e => e.Email == user.Email);
            if (isExist)
            {

                throw new Exception("User already exists. Please log in.");
            }

            User users = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Age = user.Age,

            };

            _userDbContext.Users.Add(users);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<double> AverageAgeOfUser()
        {
            var averageAge=await _userDbContext.Users.AverageAsync(e => e.Age);

            return averageAge;

        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userDbContext.Users.ToListAsync();

        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userDbContext.Users.FirstOrDefaultAsync(e => e.UserId == id);
            if (user == null)
            {

                throw new Exception("User Not Exist");
            }
            return user;

        }

        public async Task<IEnumerable<User>> GetUserByName(string nameStartwith)
        {

            var user = await _userDbContext.Users.Where(e => e.Name.StartsWith(nameStartwith)).ToListAsync();
            if (user == null)
            {

                throw new Exception("User Not Exist");

            }
            return user;
        }
    }
}
