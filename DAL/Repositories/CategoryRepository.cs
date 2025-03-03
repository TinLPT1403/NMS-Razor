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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly NewsContext newsContext;
        public CategoryRepository(NewsContext context) : base(context)
        {
            newsContext = context;
        }
        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            return await _newsContext.Categories
                .Include(c => c.ParentCategory)
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetParentCategories()
        {
            return await _newsContext.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
        }
        public async Task<IEnumerable<NewsArticle>> GetArticlesByCategoryIdAsync(int categoryId)
        {
            return await newsContext.NewsArticles
                .Where(na => na.CategoryId == categoryId && na.Category.IsActive == true)
                .ToListAsync();
        }

        public async Task RemoveAsync(Category category)
        {
            newsContext.Categories.Remove(category);
            await newsContext.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryDetailById(int id)
        {
            return await _newsContext.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _newsContext.Categories
                .Include (c => c.ParentCategory)
                .ToListAsync();
        }
    }
}
