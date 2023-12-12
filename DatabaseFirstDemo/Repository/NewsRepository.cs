using DatabaseFirstDemo.DAO;
using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.Repository
{
    public class NewsRepository : INewsRepository
    {
        public IEnumerable<News> GetAll() => NewsDao.Instance.GetAll();
        public void Insert(News news) => NewsDao.Instance.Insert(news);
        public void Update(News news) => NewsDao.Instance.Update(news);
        public News GetById(int id) => NewsDao.Instance.GetNewsById(id);
        public void Delete(News news) => NewsDao.Instance.Delete(news);
        public IEnumerable<NewsCategory> GetAllNewsCategory() => NewsDao.Instance.GetAllNewsCategory();
        public bool ChangeStatus(int id) => NewsDao.Instance.ChanceStatus(id);
        public IEnumerable<News> GetNewsByKeyword(string keyword, string sortBy, int? categoryId) => NewsDao.Instance.GetNewsByKeyword(keyword, sortBy, categoryId);
    }
}
