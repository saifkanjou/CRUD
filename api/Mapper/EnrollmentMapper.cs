using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Enrollment;
using api.Models;

namespace api.Mapper
{
    public static class EnrollmentMapper
    {
        public static Enrollment ToEnrollmentFromCreateDto(this EnrollmentDto enrollmentDto)
        {
            return new Enrollment
            {
                EnrollmentDate = enrollmentDto.EnrollmentDate,
                Status = enrollmentDto.Status,
                IsCompleted = enrollmentDto.IsCompleted
            };
        }
    }
}