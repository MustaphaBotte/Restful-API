using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer;
using DTOs;

namespace StudentApi.Controllers
{
    [Route("api/students/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("all", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StudentDTO>?>> GetAllStudentsAsync()
        {
            try
            {
                List<StudentDTO>? StudentsList = await Student.GetAllStudentsAsync();
                if (StudentsList == null || StudentsList.Count == 0)
                    return NotFound(new { message = "No student found" });

                return Ok(new { message = "success", students = StudentsList });
            }
            catch (Exception ex)
            {               
                    return Problem(ex.Message);          
            }
        }



        [HttpGet("passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StudentDTO>?>> GetPassedStudentsAsync()
        {
            try
            {
                List<StudentDTO>? PassedStudentsList = await Student.GetPassedStudentsAsync();
                if (PassedStudentsList == null || PassedStudentsList.Count == 0)
                    return NotFound(new { message = "No passed student found" });

                return Ok(new { message = "success", students = PassedStudentsList });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<double>> GetAvgGradesAsync()
        {
            try
            {
                double? AverageGrades = await Student.GetAvgGradeAsync();

                if (AverageGrades == null)
                    return NotFound(new { message = "No student found" });
                 
                return Ok(new { message = "success", AverageGrades});
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<StudentDTO?>> GetStudentByIdAsync(uint id)
        {
            try
            {
                var student = await Student.FindByIdAsync(id);
                if (student == null )
                    return NotFound(new { message = $"Student with  Id {id} not found" });

                var StudentDTO = student.StudentDTO;

                return Ok(new { message = "success", student = StudentDTO });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
