using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Reviews;
using api.Models;

namespace api.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();

        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review reviewModel);

        Task<Review?> UpdateAsync(int id, UpdateReviewDto updateDto);
        Task<Review?> DeleteAsync(int id);

    }
}