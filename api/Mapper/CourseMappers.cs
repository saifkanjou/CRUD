using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Course;
using api.Models;

namespace api.Mapper
{
    public static class CourseMappers
    {

        public static CourseDto ToCourseDto(this Course courseModel)
        {

            return new CourseDto
            {
                CourseId = courseModel.CourseId,
                Title = courseModel.Title,
                Description = courseModel.Description,
                Code = courseModel.Code,
                Credits = courseModel.Credits,
                Reviews = courseModel.Reviews.Select(r => r.ToReviewDto()).ToList() // Convert Reviews to ReviewDto
            };
        }

        public static Course ToCourseFromCreateDto(this CreateCourseDto courseDto)
        {
            return new Course
            {
                Code = courseDto.Code,
                Credits = courseDto.Credits,
                Title = courseDto.Title,
                Description = courseDto.Description
            };
        }

        public static Course ToCourseFromUpdateDto(this UpdateCourseDto courseDto)
        {
            return new Course
            {
                Code = courseDto.Code,
                Credits = courseDto.Credits,
                Title = courseDto.Title,
                Description = courseDto.Description
            };
        }
    }
}