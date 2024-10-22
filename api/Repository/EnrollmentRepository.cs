using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Enrollment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDBContext _context;

        //CTOR
        public EnrollmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<Enrollment> DeleteEnrollment(AppUser appUser, string code)
        {
            var EnrollmentModel = await _context.Enrollments.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Course.Code.ToLower() == code.ToLower());

            if (EnrollmentModel == null)
            {
                return null;
            }

            _context.Enrollments.Remove(EnrollmentModel);
            await _context.SaveChangesAsync();
            return EnrollmentModel;
        }

        public async Task<List<EnrollmentDto>> GetUserEnrollment(AppUser user)
        {
            return await _context.Enrollments
        .Where(e => e.AppUserId == user.Id)
        .Select(e => new EnrollmentDto
        {
            CourseId = e.Course.CourseId,
            Code = e.Course.Code,
            Title = e.Course.Title,
            Description = e.Course.Description,
            Credits = e.Course.Credits,
            EnrollmentDate = e.EnrollmentDate, // Add the enrollment properties here
            Status = e.Status,
            IsCompleted = e.IsCompleted
        })
        .ToListAsync();
        }

    }
}
