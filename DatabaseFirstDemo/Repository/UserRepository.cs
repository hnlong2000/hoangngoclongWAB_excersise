using DatabaseFirstDemo.Models;
using DatabaseFirstDemo.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.Repository
{
    public class UserRepository : IUsersRepository
    {
        public IEnumerable<User> GetAll() => UserDAO.Instance.GetAll();
        public void Insert(User user, UserDetail userDetail) => UserDAO.Instance.Insert(user, userDetail);
        public void Update(User user, UserDetail userDetail) => UserDAO.Instance.Update(user, userDetail);
        public User GetById(int id) => UserDAO.Instance.GetById(id);
        public void Delete(User user) => UserDAO.Instance.Delete(user);
        public IEnumerable<Role> GetAllRoles() => UserDAO.Instance.GetAllRoles();
        public bool ChangeStatus(int id) => UserDAO.Instance.ChangeStatus(id);
        public IEnumerable<UserDetail> GetUserDetailAll() => UserDAO.Instance.GetUserDetailAll();
        public void InsertUser(User user) => UserDAO.Instance.InsertUser(user);
        public void InsertUserDetail(UserDetail userDetail) => UserDAO.Instance.InsertUserDetail(userDetail);
        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
        public void UpdateUserDetail(UserDetail userDetail) => UserDAO.Instance.UpdateUserDetail(userDetail);
        public UserDetail GetByUserDetailId(int? id) => UserDAO.Instance.GetByUserDetailId(id);

        public List<UserDetail> GetUserDetailByKeyword(string keyword) => UserDAO.Instance.GetUserDetailByKeyword(keyword);
        public List<User> GetUserByKeyword(string keyword, string sortBy) => UserDAO.Instance.GetUserByKeyword(keyword, sortBy);
    }
}
