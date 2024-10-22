using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Reviews;
using api.Extensions;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Review")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<AppUser> _userManager;
        public ReviewController(ICourseRepository courseRepo, IReviewRepository reviewRepository, UserManager<AppUser> userManager)
        {
            _courseRepo = courseRepo;
            _reviewRepo = reviewRepository;
            _userManager = userManager;
        }


        //CREATE
        [HttpPost("{courseId:int}")]
        // [Authorize]

        public async Task<IActionResult> Create([FromRoute] int courseId, CreateReviewDto reviewDto)
        {
            if (!await _courseRepo.CourseExists(courseId))
            {
                return BadRequest("Course not found");
            }
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var reviewModel = reviewDto.ToReviewFromCreateDto(courseId);
            reviewModel.AppUserId = appUser.Id;
            var review = await _reviewRepo.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, review.ToReviewDto());
        }

        //READ
        [HttpGet]
        // [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            var reviewDto = reviews.Select(s => s.ToReviewDto());
            return Ok(reviewDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviewDto = review.ToReviewDto();
            return Ok(reviewDto);
        }

        //Update
        [HttpPut]
        [Route("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDto updateDto)
        {
            var reviewModel = await _reviewRepo.UpdateAsync(id, updateDto);

            if (reviewModel == null)
            {
                return NotFound();
            }

            return Ok(reviewModel.ToReviewDto());
        }

        //DELETE
        [HttpDelete("{id:int}")]
        // [Route("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var reviewModel = await _reviewRepo.DeleteAsync(id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}