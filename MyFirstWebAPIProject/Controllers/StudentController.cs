using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;
using MyFirstWebAPIProject.Service;

namespace MyFirstWebAPIProject.Controllers
{
    [Route("api/student-conroller")]
    //[Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {

        //it is constructor 
        private IStudentService studentService; //is this an object?
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
            //_studentService = studentService;
        }

        [HttpGet("GetStudents")]
        //public IEnumerable<StudentModel> GetStudents()
        //{
        //    List<StudentModel> students = studentService.GetStudent();
        //    return students;
        //}
        public IActionResult GetStudents()
        {
            if (ModelState.IsValid)
            {
                List<StudentModel> students = studentService.GetStudent();
                return Ok(students);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
           
        }

        [HttpGet("GetStudent/{Id}")]
        //public IEnumerable<StudentModel> GetByID(int Id)
        //{
        //    List<StudentModel> student = studentService.GetStudentByID(Id);
        //    return student;

        //}
        public IActionResult GetByID(int Id) {
            if (Id<=0)
            {
                return BadRequest("Provide valid Student ID.");
            }
            else
            {
                List<StudentModel> student = studentService.GetStudentByID(Id);
                if (student.Count != 0)
                {
                    return Ok(student);
                }
                else
                {
                    return NotFound("Student with ID#" + Id + " not found.");
                }
            }
        }

        //Added model validation and status code
        [HttpPost("AddStudent")]
        public IActionResult CreateStudent([FromBody] StudentModel student)
        {
            if (ModelState.IsValid)
            {
                string result_msg = studentService.AddStudent(student);
                    //return result_msg;
                return Created(string.Empty, new { Message = result_msg });

            }
            else {
                return BadRequest();
            }
        }

        //used different status code
        [HttpDelete("DelStudent/{Id}")]
        public IActionResult DeleteStudent(int Id)
        {
            List<StudentModel> res = studentService.GetStudentByID(Id);

            //when id is null
            if (Id<=0)
            {
                return BadRequest("Provide valid Student ID.");
            }
            //when there is a resource on the provided id
            if (res.Count!=0)
            {
                string result_msg = studentService.DelStudentByID(Id);
                return Ok(result_msg);
            }
            //if the id is not null but there is no resource on the given id
            else
            {
                return NotFound("Student with ID#"+Id+" not found.");
            }
          
        }


        //model validation and status code
        [HttpPut("UpdateStudent/{Id}")]
        public IActionResult UpdateStudent(int Id, [FromBody] StudentModel student)
        {
            List<StudentModel> res = studentService.GetStudentByID(Id);
            if (Id <= 0)
            {
                return BadRequest("Provide a valid Student ID.");
            }
            
            if (ModelState.IsValid) { 
                if (res.Count!=0) { 
                    string result_msg = studentService.UpdateStudent(Id, student);
                    return Ok(result_msg);
                }
                else
                {
                    return NotFound("Student not found, Couldn't Update the record!");
                }
            }
            else {  
                return BadRequest();
            }
          
            //else
            //{
            //    return BadRequest("Student with ID#"+Id+" not found.");
            //}
        }

    }
}
