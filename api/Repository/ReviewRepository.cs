using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Reviews;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
            if (review == null)
            {
                return null;
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDto updateDto)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (reviewModel == null)
            {
                return null;
            }
            reviewModel.Content = updateDto.Content;
            reviewModel.Rating = updateDto.Rating;

            await _context.SaveChangesAsync();
            return reviewModel;

        }
    }
}