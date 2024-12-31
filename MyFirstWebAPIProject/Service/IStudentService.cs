using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Service
{
    public interface IStudentService
    {
        List<StudentModel> GetStudent();
        string AddStudent(StudentModel student);
        //List<StudentModel> Addstudent(StudentModel student);
        List<StudentModel> GetStudentByID(int ID);
        string DelStudentByID(int ID);
        string UpdateStudent(int ID, StudentModel student);
    }
}
