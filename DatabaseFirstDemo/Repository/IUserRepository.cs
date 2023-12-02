using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.Repository
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();
        void Insert(User user, UserDetail userDetail);
        void Update(User user, UserDetail userDetail);
        User GetById(int id);
        void Delete(User user);
        IEnumerable<Role> GetAllRoles();
        bool ChangeStatus(int id);
        IEnumerable<UserDetail> GetUserDetailAll();
        void InsertUser(User user);
        void InsertUserDetail(UserDetail userDetail);
        void UpdateUser(User user);
        void UpdateUserDetail(UserDetail userDetail);
        UserDetail GetByUserDetailId(int? id);
        List<UserDetail> GetUserDetailByKeyword(string keyword);
        List<User> GetUserByKeyword(string keyword, string sortBy);
    }
}