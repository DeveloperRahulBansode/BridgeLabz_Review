using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using DataAcessLayer.Entity;
using DataAcessLayer.Interface;
using ModelLayer;

namespace BusinessLayer.Service
{
    public class UserQAService : IUserQAService
    {
        public readonly IUserQADataService _userQAService;
        public UserQAService(IUserQADataService userQAService) 
        { 
            _userQAService = userQAService;
        }

        public async Task AddBook(AddBookModel model)
        {
             await _userQAService.AddBook(model);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _userQAService.GetAllBooks();
        }

        public async Task<IEnumerable<User>> GetUserByNameStartWith(string letter)
        {
            return await _userQAService.GetUserByNameStartWith(letter);
        }

        public async Task<int> GetUserCount()
        {
            return await _userQAService.GetUserCount();
        }

        public async Task<IEnumerable<User>> GetUserOrderByAssending()
        {
            return await _userQAService.GetUserOrderByAssending();
        }

        public async Task<IEnumerable<User>> GetUserOrderByDesending()
        {
            return await _userQAService.GetUserOrderByDesending();
            
        }

        public async Task<IEnumerable<User>> GetUsersWithBooks()
        {
            return await _userQAService.GetUsersWithBooks();
        }
    }
}
