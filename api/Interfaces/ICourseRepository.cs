using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Course;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync(QueryObject query);
        Task<Course?> GetByIdAsync(int id);// add the (?) bc it cannot be null
        Task<Course> CreateAsync(Course courseModel);

        Task<Course?> UpdateAsync(int id, UpdateCourseDto courseDto);

        Task<Course?> DeleteAsync(int id);

        Task<bool> CourseExists(int id);

        Task<Course?> GetByCodeAsync(string code);
    }
}