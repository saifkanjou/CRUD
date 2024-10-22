using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Course;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDBContext _context; //for more privacy

        public CourseRepository(ApplicationDBContext context)
        {
            _context = context;

        }

        public async Task<bool> CourseExists(int id)
        {
            return await _context.Courses.AnyAsync(c => c.CourseId == id);
        }

        public async Task<Course> CreateAsync(Course courseModel)
        {
            await _context.Courses.AddAsync(courseModel);
            await _context.SaveChangesAsync();
            return courseModel;
        }

        public async Task<Course?> DeleteAsync(int id)
        {
            var courseModel = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (courseModel == null) return null;
            _context.Courses.Remove(courseModel);
            await _context.SaveChangesAsync();

            return courseModel;
        }

        public async Task<List<Course>> GetAllAsync(QueryObject query)
        {
            var coursesQuery = _context.Courses.Include(c => c.Reviews).ThenInclude(a => a.AppUser).AsQueryable();
            //FILTERING
            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                coursesQuery = coursesQuery.Where(x => x.Code.Contains(query.Code));
            }

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                coursesQuery = coursesQuery.Where(x => x.Title.Contains(query.Title));
            }

            //SORTING
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    coursesQuery = query.IsDescinding ? coursesQuery.OrderByDescending(s => s.Title) : coursesQuery.OrderBy(s => s.Title);
                }

                if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    coursesQuery = query.IsDescinding ? coursesQuery.OrderByDescending(s => s.Code) : coursesQuery.OrderBy(s => s.Code);
                }
                if (query.SortBy.Equals("Credits", StringComparison.OrdinalIgnoreCase))
                {
                    coursesQuery = query.IsDescinding ? coursesQuery.OrderByDescending(s => s.Credits) : coursesQuery.OrderBy(s => s.Credits);
                }
            }

            //Pagination
            var skipNumber = (query.PageNumber - 1) * (query.PageSize);
            var takeNumber = query.PageSize;

            return await coursesQuery.Skip(skipNumber).Take(takeNumber).ToListAsync();
        }

        public async Task<Course?> GetByCodeAsync(string code)
        {
            return await _context.Courses.FirstOrDefaultAsync(s => s.Code == code);
        }


        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Reviews).ThenInclude(a => a.AppUser).FirstOrDefaultAsync(i => i.CourseId == id);
        }

        public async Task<Course?> UpdateAsync(int id, UpdateCourseDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            if (course == null) return null;
            course.Title = courseDto.Title;
            course.Code = courseDto.Code;
            course.Description = courseDto.Description;
            course.Credits = courseDto.Credits;
            await _context.SaveChangesAsync();
            return course;
        }
    }
}