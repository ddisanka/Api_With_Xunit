using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Model;
using CoreServices.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepository studentRepository;
        public StudentController(IStudentRepository _postRepository)
        {
            studentRepository = _postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await studentRepository.GetStudents();
                if (students == null)
                {
                    return NotFound();
                }

                return Ok(students);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

       [HttpGet ("{id}")]
        public async Task<IActionResult> GetStudent(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var students = await studentRepository.GetStudent(id);

                if (students == null)
                {
                    return NotFound();
                }

                return Ok(students);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody]Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stdId = await studentRepository.AddStudent(model);
                    if (stdId > 0)
                    {
                        return Ok(stdId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        //[HttpPost]
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            int result = 0;

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await studentRepository.DeleteStudent(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateStudent([FromBody]Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await studentRepository.UpdateStudent(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}