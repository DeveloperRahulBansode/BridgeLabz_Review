using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entity;
using ModelLayer;

namespace DataAcessLayer.Interface
{
   public  interface IUserQADataService
    {
        Task<IEnumerable<User>> GetUserByNameStartWith(string letter);
        Task<int> GetUserCount();

        Task<IEnumerable<User>> GetUserOrderByAssending();
        Task<IEnumerable<User>> GetUserOrderByDesending();
        Task<IEnumerable<User>> GetUsersWithBooks();
        Task AddBook(AddBookModel model);
        Task<IEnumerable<Book>> GetAllBooks();








    }
}
