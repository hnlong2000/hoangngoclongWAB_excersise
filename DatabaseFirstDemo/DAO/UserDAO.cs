using DatabaseFirstDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.DAO
{
    public class UserDAO
    {
        private static UserDAO instance;
        private static readonly object instanceLock = new object();
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public List<User> GetAll()
        {
            List<User> user;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                user = stock.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public List<UserDetail> GetUserDetailByKeyword(string keyword)
        {
            List<UserDetail> user;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                user = stock.UserDetails.Where(p => p.FullName.ToLower().Contains(keyword) || p.Address.ToLower().Contains(keyword)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public List<User> GetUserByKeyword(string keyword, string sortBy)
        {
            List<User> users = new List<User>();
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                var usersQuery = stock.Users
            .Join(stock.UserDetails,
                user => user.Id,
                detail => detail.UserId,
                (user, detail) => new { User = user, Detail = detail });
                if (!String.IsNullOrEmpty(keyword))
                {
                    usersQuery = stock.Users
                        .Join(stock.UserDetails,
                            user => user.Id,
                            detail => detail.UserId,
                            (user, detail) => new { User = user, Detail = detail })
                        .Where(u => u.Detail.FullName.ToLower().Contains(keyword)
                                    || u.Detail.Address.ToLower().Contains(keyword));
                }

                switch (sortBy)
                {
                    case "name":
                        usersQuery = usersQuery.OrderBy(o => o.User.UserName);
                        break;
                    case "namedesc":
                        usersQuery = usersQuery.OrderByDescending(o => o.User.UserName);
                        break;
                    case "fullname":
                        usersQuery = usersQuery.OrderBy(o => o.Detail.FullName);
                        break;
                    case "fullnamedesc":
                        usersQuery = usersQuery.OrderByDescending(o => o.Detail.FullName);
                        break;
                    case "address":
                        usersQuery = usersQuery.OrderBy(o => o.Detail.Address);
                        break;
                    case "addressdesc":
                        usersQuery = usersQuery.OrderByDescending(o => o.Detail.Address);
                        break;
                    default:
                        break;
                }
                users = usersQuery.Select(u => u.User).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public User GetById(int? id)
        {
            User user;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                user = stock.Users.SingleOrDefault(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public UserDetail GetByUserDetailId(int? id)
        {
            UserDetail userDetail;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                userDetail = stock.UserDetails.SingleOrDefault(r => r.UserId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userDetail;
        }

        public List<UserDetail> GetUserDetailAll()
        {
            List<UserDetail> listUserDetail;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                listUserDetail = stock.UserDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUserDetail;
        }

        public void Insert(User user, UserDetail userDetail)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {

                    stock.Add(user);
                    stock.Add(userDetail);
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void InsertUser(User user)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {

                    stock.Add(user);
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void InsertUserDetail(UserDetail user)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {

                    stock.Add(user);
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Update(User user, UserDetail userDetail)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {
                    stock.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    stock.Entry<UserDetail>(userDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateUser(User user)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {
                    stock.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateUserDetail(UserDetail userDetail)
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction())
            {
                try
                {
                    stock.Entry<UserDetail>(userDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    stock.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            using Batch177179Context stock = new Batch177179Context();
            return stock.Roles.ToList();
        }

        public void Delete(User user)
        {
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                var us = stock.Users.SingleOrDefault(c => c.Id == user.Id);
                var usdt = stock.UserDetails.SingleOrDefault(c => c.UserId == user.Id);
                stock.Remove(usdt);
                stock.Remove(us);
                stock.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatus(int id)
        {
            using Batch177179Context stock = new Batch177179Context();
            var user = stock.Users.Find(id);
            user.Status = !user.Status;
            stock.SaveChanges();
            return (bool)user.Status;
        }
    }
}