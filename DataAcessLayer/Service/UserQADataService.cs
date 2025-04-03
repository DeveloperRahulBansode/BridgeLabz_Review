using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Context;
using DataAcessLayer.Entity;
using DataAcessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace DataAcessLayer.Service
{
    public class UserQADataService : IUserQADataService
    {

        public readonly UserDbContext _userDbContext;
        public UserQADataService(UserDbContext userDbContext) 
        {
            _userDbContext = userDbContext;
        
        }

        public async Task<IEnumerable<User>> GetUserByNameStartWith(string letter)
        {
            var userData=await _userDbContext.Users.Where(e=>e.Name.StartsWith(letter)).ToListAsync();

            if (userData == null)
            {
                throw new Exception("User Not Found");

            }
            return userData;
        }

        public async Task<int> GetUserCount()
        {
            var userCount=await _userDbContext.Users.CountAsync();

            if (userCount==0) {
                throw new Exception("There is No User ");
                    
            }
            return userCount;
        }

        public async Task<IEnumerable<User>> GetUserOrderByAssending()
        {
            var users = await _userDbContext.Users.OrderBy(e => e.Name).ToListAsync();
            if (users == null)
            {
                throw new Exception("User Not Found");
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUserOrderByDesending()
        {
            var users = await _userDbContext.Users.OrderByDescending(e => e.Name).ToListAsync();
            if (users == null)
            {
                throw new Exception("User Not Found");
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersWithBooks()
        {
            var usersWithBooks = await _userDbContext.Users.Include(u => u.Books).ToListAsync();
            if (usersWithBooks == null)
            {
                throw new Exception("User Not Found");
            }
            return usersWithBooks;

        }

        public async Task AddBook(AddBookModel model)
        {
            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Price=model.Price,
                UserId = model.UserId
            };

            _userDbContext.Books.Add(book);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _userDbContext.Books.ToListAsync();
            if (books == null)
            {
                throw new Exception("Books Not Found");
            }
            return books;
        }
    }
}
