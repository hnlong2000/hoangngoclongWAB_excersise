using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.DAO
{
    public class NewsDao
    {
        private static NewsDao instance;
        private static readonly object instanceLock = new object();
        public static NewsDao Instance
        {
            get
            {
                lock (instanceLock) 
                {
                    if(instance == null)
                    {
                        instance = new NewsDao();
                    }
                }
                return instance; 
            }
        }

        public List<News> GetAll() 
        {
            List<News> news;
            try 
            {
                using Batch177179Context stock = new Batch177179Context();
                news = stock.News.ToList();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            return news;
        }

        public IEnumerable<News> GetNewsByKeyword(string keyword, string sortBy, int? categoryId)
        {
            List<News> news = new List<News>();
            try
            {
                Batch177179Context stock = new Batch177179Context();
                IQueryable<News> newsQuery = stock.News;
                if (!String.IsNullOrEmpty(keyword))
                {
                    newsQuery = newsQuery.Where(u => u.Title != null && u.Title.ToLower().Contains(keyword)); // Remove ToList() here
                }

                switch (sortBy)
                {
                    case "title":
                        newsQuery = (Microsoft.EntityFrameworkCore.DbSet<News>)newsQuery.OrderBy(o => o.Title);
                        break;
                    case "titledesc":
                        newsQuery = (Microsoft.EntityFrameworkCore.DbSet<News>)newsQuery.OrderByDescending(o => o.Title);
                        break;
                    default:
                        break;
                }
                if (categoryId != null)
                {
                    news = newsQuery.Where(u => u.CategoryId == categoryId).ToList();
                }
                else
                {
                    news = newsQuery.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return news;
        }

        public News GetNewsById(int id) 
        {
            News news;
            try 
            {
                using Batch177179Context stock = new Batch177179Context();
                news = stock.News.SingleOrDefault(n => n.Id == id);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);    
            }
            return news;
        }

        public void Insert(News news) 
        {
            using Batch177179Context stock = new Batch177179Context();
            using (var transaction = stock.Database.BeginTransaction()) 
            {
                try
                {
                    stock.Add(news);
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

        public void Update(News news) 
        {
            using Batch177179Context stock =  new Batch177179Context();
            using(var transaction = stock.Database.BeginTransaction())
            {
                try
                {
                    stock.Update(news);
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

        public IEnumerable<NewsCategory> GetAllNewsCategory()
        {
            using Batch177179Context stock = new Batch177179Context();
            return stock.NewsCategories.ToList();
        }

        public void Delete(News news) 
        {
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                var us = stock.News.Where(n => n.Id == news.Id);
                stock.Remove(news);
                stock.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChanceStatus(int id)
        {
            using Batch177179Context batch= new Batch177179Context();
            var news = batch.News.Find(id);
            news.Status = !news.Status;
            batch.SaveChanges();
            return (bool)news.Status;
        }
    }
}
