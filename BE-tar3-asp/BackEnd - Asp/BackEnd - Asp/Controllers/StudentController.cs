using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackEnd___Asp.DTO;
using CafetriaDBLibrary___Asp.EF;


namespace BackEnd___Asp.Controllers
{
    public class StudentController : ApiController
    {
        // GET api/<controller>
        public List<StudentDTO> Get()
        {
            CafeteriaDbContext db = new CafeteriaDbContext();
            var students = db.Students.Select(s => new StudentDTO()
            {
                StudentId = s.StudentId,
                Name = s.Name,
                IsActive = s.IsActive,
                AvgGrade = s.AvgGrade
            }).ToList();
            return students;
        }
    }
}