using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Reviews;
using api.Models;

namespace api.Mapper
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                Id = reviewModel.Id,
                Content = reviewModel.Content,
                Rating = reviewModel.Rating,
                CreatedOn = reviewModel.CreatedOn,
                CourseId = reviewModel.CourseId,

                CreatedBy = reviewModel.AppUser.UserName,
            };
        }
        public static Review ToReviewFromCreateDto(this CreateReviewDto courseDto, int courseId)
        {
            return new Review
            {
                Content = courseDto.Content,
                Rating = courseDto.Rating,
                CourseId = courseId
            };
        }

    }
}



