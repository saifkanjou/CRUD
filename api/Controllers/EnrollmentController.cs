using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Enrollment;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ICourseRepository _courseRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        public EnrollmentController(UserManager<AppUser> userManager, ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo)
        {
            _userManager = userManager;
            _courseRepo = courseRepo;
            _enrollmentRepo = enrollmentRepo;

        }


        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetUserEnrollment()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            //find the user's enrollment
            var userEnrollment = await _enrollmentRepo.GetUserEnrollment(appUser);
            return Ok(userEnrollment);
        }



        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> AddEnrollment(string code, CreateEnrollmentDto enrollmentDto)
        {
            //1st step get the user
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            //2nd step get the course
            var course = await _courseRepo.GetByCodeAsync(code);

            if (course == null) return BadRequest("Course not found");

            var userEnrollment = await _enrollmentRepo.GetUserEnrollment(appUser);

            //check if we already have the course added
            if (userEnrollment.Any(e => e.Code.ToLower() == code.ToLower())) return BadRequest("Cannot add same Course to enrollment");
            // 3rd step: Create the enrollment object
            var enrollmentModel = new Enrollment
            {
                CourseID = course.CourseId,
                AppUserId = appUser.Id,
                EnrollmentDate = DateTime.UtcNow,
                Status = enrollmentDto.Status, // Use the status from the DTO
                IsCompleted = enrollmentDto.IsCompleted // Use the IsCompleted flag from the DTO
            };

            var result = await _enrollmentRepo.CreateAsync(enrollmentModel);

            // Check if the creation was successful
            if (result == null)
            {
                return StatusCode(500, "Could not create the enrollment");
            }
            // Map the created enrollment to the EnrollmentDto
            var createdDto = new EnrollmentDto
            {
                CourseId = course.CourseId,
                Code = course.Code,
                Title = course.Title,
                Description = course.Description,
                Credits = course.Credits,
                EnrollmentDate = enrollmentModel.EnrollmentDate,
                Status = enrollmentModel.Status,
                IsCompleted = enrollmentModel.IsCompleted
            };

            if (enrollmentModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        // [Authorize]

        public async Task<IActionResult> DeleteEnrollment(string code)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            //get all the the courses in the userEnrollment
            var userEnrollment = await _enrollmentRepo.GetUserEnrollment(appUser);

            //check if the course is there
            var filteredCourse = userEnrollment.Where(s => s.Code.ToLower() == code.ToLower()).ToList();


            if (filteredCourse.Count() == 1)
            {
                await _enrollmentRepo.DeleteEnrollment(appUser, code);
            }
            else
            {
                return BadRequest("Course not in your enrollment");
            }

            return Ok();
        }
    }
}
