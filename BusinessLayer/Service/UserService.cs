using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using DataAcessLayer.Entity;
using DataAcessLayer.Interface;

namespace BusinessLayer.Service
{
    public class UserService : IUserService
    {
        public readonly IUserDataService _userDataService;
        public UserService(IUserDataService userDataService) 
        {
            _userDataService = userDataService;
        
        }


        public async Task AddUser(User user)
        {
            await _userDataService.AddUser(user);
        }

        public async Task<double> AverageAgeOfUser()
        {
            return await _userDataService.AverageAgeOfUser();
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userDataService.GetAllUser();

        }

        public async Task<User> GetUserById(int id)
        {
            return await _userDataService.GetUserById(id);

        }

        public async Task<IEnumerable<User>> GetUserByName(string nameStartwith)
        {
            return await _userDataService.GetUserByName(nameStartwith);

        }
    }
}
