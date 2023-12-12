using DatabaseFirstDemo.DAO;
using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.Repository
{
    public class UsersRepository : IUsersRepository
    {
        public IEnumerable<User> GetAll() => UsersDao.Instance.GetAll();
        public void Insert(User user, UserDetail userDetail) => UsersDao.Instance.Insert(user, userDetail);
        public void Update(User user, UserDetail userDetail) => UsersDao.Instance.Update(user, userDetail);
        public User GetById(int id) => UsersDao.Instance.GetById(id);
        public void Delete(User user) => UsersDao.Instance.Delete(user);
        public IEnumerable<Role> GetAllRoles() => UsersDao.Instance.GetAllRoles();
        public bool ChangeStatus(int id) => UsersDao.Instance.ChangeStatus(id);
        public IEnumerable<UserDetail> GetUserDetailAll() => UsersDao.Instance.GetUserDetailAll();
        public void InsertUser(User user) => UsersDao.Instance.InsertUser(user);
        public void InsertUserDetail(UserDetail userDetail) => UsersDao.Instance.InsertUserDetail(userDetail);
        public void UpdateUser(User user) => UsersDao.Instance.UpdateUser(user);
        public void UpdateUserDetail(UserDetail userDetail) => UsersDao.Instance.UpdateUserDetail(userDetail);
        public UserDetail GetByUserDetailId(int? id) => UsersDao.Instance.GetByUserDetailId(id);

        public List<UserDetail> GetUserDetailByKeyword(string keyword) => UsersDao.Instance.GetUserDetailByKeyword(keyword);
        public List<User> GetUserByKeyword(string keyword, string sortBy, int? roleId) => UsersDao.Instance.GetUserByKeyword(keyword, sortBy, roleId);
        public User CheckLogin(string username, string password) => UsersDao.Instance.CheckLogin(username, password);
        public User GetByUserName(string userName) => UsersDao.Instance.GetByUserName(userName);
    }
}