using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Course;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }


        //READ ALL
        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var courses = await _courseRepo.GetAllAsync(query);
            var CourseDto = courses.Select(s => s.ToCourseDto()).ToList();
            return Ok(CourseDto);
        }

        //get a single item
        [HttpGet("{id:int}")]
                // [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course.ToCourseDto());
        }


        //CREATE
        [HttpPost]
                [Authorize]

        public async Task<IActionResult> Create([FromBody] CreateCourseDto courseDto)
        {
            var courseModel = courseDto.ToCourseFromCreateDto();
            await _courseRepo.CreateAsync(courseModel);
            return CreatedAtAction(nameof(GetById), new { id = courseModel.CourseId }, courseModel.ToCourseDto());

        }

        //UPDATE
        //PUT (UPDATE)
        [HttpPut]
        [Route("{id:int}")]
                        [Authorize]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCourseDto updateDto)
        {
            var courseModel = await _courseRepo.UpdateAsync(id, updateDto);
            if (courseModel == null)
            {
                return NotFound();
            }
            return Ok(courseModel.ToCourseDto());
        }

        //DELETE
        [HttpDelete]
        [Route("{id:int}")]
                        [Authorize]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var course = await _courseRepo.DeleteAsync(id);
            if (course == null) return NotFound();

            return NoContent();
        }
    }
}