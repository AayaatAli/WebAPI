using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyFirstWebAPIProject.Models;


namespace MyFirstWebAPIProject.Service
{
    public class StudentService : IStudentService
    {
        private IConfiguration Configuration;

        public StudentService(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }



        public List<StudentModel> GetStudent()
        {
            List<StudentModel> res = new List<StudentModel>();
            //var students = new List<StudentModel>();

            //SqlConnection con = null;
            string ConnectionString = Configuration.GetConnectionString("TrainingDB");

            //string ConnectionString = "Server=sofsrvdb2016;Database=TrainingDB;User Id=sa;Password=Sofcom!@#;TrustServerCertificate=True";
            //con = new SqlConnection(ConnectionString);

            //StringBuilder query = new StringBuilder();
            //query.AppendLine(" Select * from Aayaat_Std");

            //StringBuilder query1 = new StringBuilder();
            //query1.AppendLine("INSERT INTO Aayaat_Std (First_Name, Last_Name, Age, Grade) VALUES ('Hijab', 'Ali', , '14', 'A+');");

            string query = "Select * from Aayaat_Std";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                //cmd.CommandText = query.ToString();
                // Execute the query
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new StudentModel
                        {
                            Id = reader.GetInt32(0), // Column index
                            First_Name = reader.GetString(1),
                            Last_Name = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Grade = reader.GetString(4)
                        };

                        res.Add(student);
                    }
                }
            }

            return res;

        }
        public List<StudentModel> GetStudentByID(int ID)
        {
            List<StudentModel> res = new List<StudentModel>();
            string ConnectionString = Configuration.GetConnectionString("TrainingDB");
            //string ConnectionString = "Server=sofsrvdb2016;Database=TrainingDB;User Id=sa;Password=Sofcom!@#;TrustServerCertificate=True";
            string query = "Select * from Aayaat_Std where id=" + ID;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new StudentModel
                        {
                            Id = reader.GetInt32(0), // Column index
                            First_Name = reader.GetString(1),
                            Last_Name = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Grade = reader.GetString(4)
                        };

                        res.Add(student);
                    }
                }
            }
            return res;
        }

        public string DelStudentByID(int ID)
        {

            try
            {
                string ConnectionString = Configuration.GetConnectionString("TrainingDB");
                //string ConnectionString = "Server=sofsrvdb2016;Database=TrainingDB;User Id=sa;Password=Sofcom!@#;TrustServerCertificate=True";
                string query = "DELETE FROM Aayaat_Std WHERE id=" + ID;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int rows_affected = cmd.ExecuteNonQuery();
                    if (rows_affected > 0)
                    {
                        return "Student record#" + ID + " deleted!";
                    }
                    else
                    {
                        return "Record not found!";
                    }
                }

            }
            catch (Exception ex)
            {
                return "Opps Something Went Wrong! " + ex.ToString();

            }
            //}
        }

        public string AddStudent(StudentModel student)
        {
         
                try
                {
                    string ConnectionString = Configuration.GetConnectionString("TrainingDB");
                    string query = "INSERT INTO Aayaat_Std(First_Name, Last_Name, Age, Grade) VALUES('" + student.First_Name + "','" + student.Last_Name + "','" + student.Age + "','" + student.Grade + "')";
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        int rows_affected = cmd.ExecuteNonQuery();
                        if (rows_affected > 0)
                        {
                            return "New Student added successfully!";
                        }
                        else
                        {
                            return "Student not added!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "Opps Something Went Wrong! " + ex.ToString();

                }

            }
        



        public string UpdateStudent(int ID, StudentModel student)
        {
            try
            {
                string ConnectionString = Configuration.GetConnectionString("TrainingDB");
                //string ConnectionString = "Server=sofsrvdb2016;Database=TrainingDB;User Id=sa;Password=Sofcom!@#;TrustServerCertificate=True";
                string query = "UPDATE Aayaat_Std SET First_Name='" + student.First_Name + "', Last_Name='" + student.Last_Name + "', Age='" + student.Age + "',Grade='" + student.Grade + "' WHERE id=" + ID;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int rows_affected = cmd.ExecuteNonQuery();
                    if (rows_affected > 0)
                    {
                        return "Updated the record of Student#" + ID;
                    }
                    else
                    {
                        return "Student not found, Couldn't Update the record!";
                    }
                }

            }
            catch (Exception ex)
            {
                return "Opps Something Went Wrong! " + ex.ToString();
            }
        }
    }
}
