using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseFirstDemo.Models;

namespace DatabaseFirstDemo.Repository
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAll();
        void Insert(News news);
        void Update(News news);
        News GetById(int id);
        void Delete(News news);
        IEnumerable<NewsCategory> GetAllNewsCategory();
        bool ChangeStatus(int id);
        IEnumerable<News> GetNewsByKeyword(string keyword, string sortBy, int? categoryId);
    }
}
