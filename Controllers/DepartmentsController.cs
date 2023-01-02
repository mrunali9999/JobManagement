using JobManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JobManagement.Models.ModelClass;

namespace JobManagement.Controllers
{
    public class DepartmentsController : ApiController
    {
        JobManagementEntities db = new JobManagementEntities();

        //Create API -Post
        [HttpPost]
        public IHttpActionResult CreateDepartment([FromBody] Department department)
        {
            Department dept = new Department();
            dept.Title = department.Title;
            db.Departments.Add(dept);
            db.SaveChanges();
            return Ok();
        }
        //Update API -PUT

        public IHttpActionResult UpdateDepartment(int id, [FromBody] Department department)
        {
            var entity = db.Departments.FirstOrDefault(t => t.Id == id);
            if (entity == null)
            {
                BadRequest("Invalid Id");
            }
            entity.Title = department.Title;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();

        }

        //Details API -Get
        [HttpGet]
        public IHttpActionResult GetDepartments()
        {

            List<Department> departmentList = db.Departments.ToList();

            return Ok(departmentList);
        }

    }
}
