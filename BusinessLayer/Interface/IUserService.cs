using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IUserService
    {
        Task AddUser(User user);

        Task<IEnumerable<User>> GetAllUser();

        Task<User> GetUserById(int id);

        Task<IEnumerable<User>> GetUserByName(string nameStartwith);

        Task<Double> AverageAgeOfUser();
    }
}
