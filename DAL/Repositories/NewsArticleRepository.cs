using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class NewsArticleRepository : GenericRepository<NewsArticle>, INewsArticleRepository
    {
        private readonly NewsContext newsContext;
        public NewsArticleRepository(NewsContext context) : base(context)
        {
            newsContext = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllByCategoryIdAsync(int id)
        {
            return await newsContext.NewsArticles
                .Where(article => article.CategoryId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetActiveNewsArticlesAsync()
        {
            return await newsContext.NewsArticles
                .Include(article => article.Category)   // Include Category to access CategoryDescription
                .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                .Include(article => article.NewsTags)
                .ThenInclude(newstags => newstags.Tag)
                .Where(article => article.NewsStatus == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<NewsArticle>> GetActiveNewsArticlesByUserIdAsync(int userId)
        {
            return await newsContext.NewsArticles
                .Where(article => article.CreatedBy.AccountId == userId)
                .Include(article => article.Category)   // Include Category to access CategoryDescription
                .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                .Include(article => article.NewsTags)
                .ThenInclude(newstags => newstags.Tag)
                .Where(article => article.NewsStatus == true)
                .ToListAsync();
        }

        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            return await newsContext.NewsArticles
                .Include(article => article.Category)   // Include Category to access CategoryDescription
                .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                .Include(article => article.NewsTags)
                .ThenInclude(newstags => newstags.Tag)
                .FirstOrDefaultAsync(article => article.NewsArticleId == id);
        }

        public async Task<IEnumerable<NewsArticle>> GetAllArticlesAsync()
        {
            return await _newsContext.NewsArticles
                            .Include(article => article.Category)   // Include Category to access CategoryDescription
                            .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                            .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                            .Include(article => article.NewsTags)
                            .ThenInclude(newstags => newstags.Tag)
                            .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetArticlesWithActiveCategoriesAsync()
        {
            return await _newsContext.NewsArticles
                             .Include(article => article.Category)   // Include Category to access CategoryDescription
                            .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                            .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                            .Include(article => article.NewsTags)
                            .ThenInclude(newstags => newstags.Tag)
                            .Where(article => article.Category.IsActive == true && article.NewsStatus == true)
                            .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> SearchArticlesAsync(string? searchTerm, int? categoryId, int? tagId)
        {
            var query = _newsContext.NewsArticles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => a.NewsTitle.Contains(searchTerm) || a.Headline.Contains(searchTerm));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }

            if (tagId.HasValue)
            {
                query = query.Where(a => a.NewsTags.Any(t => t.TagId == tagId));
            }
            return await query
                .Include(article => article.Category)
                .Include(article => article.CreatedBy) 
                .Include(article => article.UpdatedBy)  
                .Include(article => article.NewsTags)
                .ThenInclude(newstags => newstags.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetNewsArticlesReportAsync(DateTime startDate, DateTime endDate)
        {
            return await _newsContext.NewsArticles
                 .Include(article => article.Category)   // Include Category to access CategoryDescription
                            .Include(article => article.CreatedBy)  // Include CreatedBy to access AccountId
                            .Include(article => article.UpdatedBy)  // Include UpdatedBy to access AccountId
                            .Include(article => article.NewsTags)
                            .ThenInclude(newstags => newstags.Tag)
                            .Where(article => article.CreatedDate >= startDate && article.CreatedDate <= endDate)
                            .ToListAsync();


        }
    }
}
