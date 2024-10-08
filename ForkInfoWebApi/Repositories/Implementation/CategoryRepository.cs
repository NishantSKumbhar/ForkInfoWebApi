﻿using ForkInfoWebApi.Data;
using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ForkInfoWebApi.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();



            return category;
        }

       
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
            
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var Samplecategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if(Samplecategory != null)
            {
                dbContext.Entry(Samplecategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCategory != null)
            {
                dbContext.Categories.Remove(existingCategory);
                await dbContext.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }

    }
}
