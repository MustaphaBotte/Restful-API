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
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private readonly string[] _allowedContentTypes = { "image/jpeg", "image/png", "image/gif", "image/webp" };
        private readonly string SavingPath = @"..\Pictures";

        private bool IsValidImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedContentTypes.Contains(file.ContentType)
                && _allowedExtensions.Contains(extension);
        }

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




        [HttpPost("create", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO?>> AddNewStudent(StudentDTO UserInfo)
        {
            if(string.IsNullOrEmpty(UserInfo.Name) || UserInfo.Grade<0 || UserInfo.Grade>100 || UserInfo.Age <= 0)
            {
                return Problem($"The following fields : name , age and grade must contain a valid values ");
            }
            try
            {
                var NewStudent = new Student(UserInfo);
                if (await NewStudent.Save())
                {
                    UserInfo.ID = NewStudent.ID;
                    return CreatedAtAction($"GetStudentById", 
                        routeValues:new { id = NewStudent.ID },
                        value : new { message = $"user created successfully with id {NewStudent.ID}",student= UserInfo });
                }
                return Problem("Failed to save the student please try again.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO?>> UpdateStudent(uint id, StudentDTO updatedStudent)
        {
            if (string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Grade < 0
                              || updatedStudent.Grade > 100 || updatedStudent.Age <= 0)
            {
                return BadRequest(new { message = $"The following fields : name , age and grade must contain a valid values " });
            }
            try
            {
                var student = await Student.FindByIdAsync(id);
                if (student == null)
                    return NotFound(new { message = $"Student with  Id {id} not found" });

                student.Name  = updatedStudent.Name;
                student.Age   = updatedStudent.Age;
                student.Grade = updatedStudent.Grade;

                if (await student.Save())
                {
                    return Ok(new { message = "Student updated successfully", student = student.StudentDTO });
                }
                return Problem("Failed to save the student please try again.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return NotFound(new { message = $"id with {id} is not valid" });
            }
            try
            {
                if (await Student.DeleteStudentAsync(id))
                    return NoContent();

                return NotFound(new { message = $"Student with  Id {id} not found" });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost("image/upload", Name = "UploadPicture")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<string>> SaveProfilePicture(IFormFile ImgFile)
        {
            if (ImgFile == null ||ImgFile.Length == 0)
                return BadRequest(new { message = "Profile picture cannot be empty" });

            if (ImgFile.Length> 1_048_576)
                return BadRequest(new { message = "Profile picture must be less than 1mb" });


            if (!IsValidImage(ImgFile))
                return BadRequest(new { message = "Only image files are allowed" });


            try
            {

                if (!Directory.Exists(SavingPath))
                    Directory.CreateDirectory(SavingPath);

                string FileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgFile.FileName);
                string FullFilePath = Path.Combine(SavingPath, FileName);

                await using Stream stream = new FileStream(FullFilePath, FileMode.Create);
               
                await ImgFile.CopyToAsync(stream);
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var imageUrl = $"{baseUrl}/api/students/image/{FileName}";
                return CreatedAtRoute("GetPicture",
                    routeValues: new { pictureId = FileName },
                    new { message = "Profile Picture Saved Successfully", imgPath= imageUrl });

            }
            catch
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }


        }



        [HttpGet("image/{pictureId}", Name = "GetPicture")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProfilePicture(string pictureId)
        {
            if (string.IsNullOrEmpty(pictureId))
                return NotFound(new { message = "Profile picture id cannot be empty" });



            string FullFilePath = Path.Combine(SavingPath, pictureId);
            if (!System.IO.File.Exists(FullFilePath))
                return NotFound(new { message ="Image not found" });

            try
            {
                byte[] pictureBytes = await System.IO.File.ReadAllBytesAsync(FullFilePath);
                return File(pictureBytes, GetMimeType(FullFilePath));
            }
            catch
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }


        }
        private string GetMimeType(string FullFilePath)
        {
            string extention = Path.GetExtension(FullFilePath);
            return extention switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }

    }
}
