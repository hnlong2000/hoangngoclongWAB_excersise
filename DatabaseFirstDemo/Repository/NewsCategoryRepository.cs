using DatabaseFirstDemo.DAO;
using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.Repository
{
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        public IEnumerable<NewsCategory> GetAll()=> NewsCategoryDao.Instance.GetAll();
        public void Insert(NewsCategory newsCategory)=> NewsCategoryDao.Instance.Insert(newsCategory);
        public void Update(NewsCategory newsCategory) => NewsCategoryDao.Instance.Update(newsCategory);
        public NewsCategory GetById(int id) => NewsCategoryDao.Instance.GetById(id);
        public void Delete(NewsCategory newsCategory) => NewsCategoryDao.Instance.Delete(newsCategory);
    }
}
