using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Enrollment;
using api.Models;

namespace api.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentDto>> GetUserEnrollment(AppUser user);
        Task<Enrollment> CreateAsync(Enrollment enrollment);

         Task<Enrollment> DeleteEnrollment(AppUser appUser, string code);
    }
}