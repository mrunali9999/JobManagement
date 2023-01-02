using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JobManagement.Models;
using static JobManagement.Models.ModelClass;

namespace JobManagement.Controllers
{
    public class JobsController : ApiController
    {
        JobManagementEntities db = new JobManagementEntities();

        //Create API -Post
        [HttpPost]
        public IHttpActionResult CreateJobOpening([FromBody] JobOpeningView job)
        {
            JobOpening jobOpening = new JobOpening();
            jobOpening.Title = job.Title;
            jobOpening.Description = job.Description;
            jobOpening.LocationId = job.LocationId;
            jobOpening.DepartmentId = job.DepartmentId;
            jobOpening.ClosingDate = job.ClosingDate;
            jobOpening.PostedDate = DateTime.Now;

            db.JobOpenings.Add(jobOpening);
            db.SaveChanges();
            jobOpening.Code = "JOB-" + jobOpening.Id;
            db.Entry(jobOpening).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        //Update API -PUT

        public IHttpActionResult UpdateJobOpening(int id, [FromBody] JobOpening jobOpening)
        {
            var entity = db.JobOpenings.FirstOrDefault(t => t.Id == id);
            if (entity == null)
            {
                BadRequest("Invalid Id");
            }
            entity.Title = jobOpening.Title;
            entity.Description = jobOpening.Description;
            entity.LocationId = jobOpening.LocationId;
            entity.DepartmentId = jobOpening.DepartmentId;
            entity.ClosingDate = jobOpening.ClosingDate;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();

        }

        //Details API -Get
        [HttpGet]
        public IHttpActionResult GetJobOpeningDetails(int id)
        {
            
            JobOpeningDetails jobOpeningList = (from jo in db.JobOpenings
                                                      join l in db.Locations on jo.LocationId equals l.Id
                                                      join d in db.Departments on jo.DepartmentId equals d.Id
                                                      select new JobOpeningDetails
                                                      {
                                                          Id = jo.Id,
                                                          Code = jo.Code,
                                                          Title = jo.Title,
                                                          Description = jo.Description,
                                                          Location = l,
                                                          Department = d,
                                                          PostedDate = jo.PostedDate,
                                                          ClosingDate = jo.ClosingDate
                                                      }).Where(m=>m.Id==id).FirstOrDefault();
            return Ok(jobOpeningList);
        }

        [HttpGet]
        public IHttpActionResult List([FromBody] PagedSearchRequest pagedSearchRequest)
        {
            var skipAmount=pagedSearchRequest.PageSize*(pagedSearchRequest.PageNo-1);
            List<Job> jobList = (from jo in db.JobOpenings
                                 join l in db.Locations on jo.LocationId equals l.Id
                                 join d in db.Departments on jo.DepartmentId equals d.Id
                                 select new Job
                                 {
                                     Id = jo.Id,
                                     Code = jo.Code,
                                     Title = jo.Title,
                                     Location = l.Title,
                                     Department = d.Title,
                                     PostedDate = jo.PostedDate,
                                     ClosingDate = jo.ClosingDate
                                 }
                               ).OrderBy(m=>m.Id).Skip(skipAmount).Take(pagedSearchRequest.PageSize).ToList();
            PagedResults pagedResults = new PagedResults();
            pagedResults.Data = jobList;
            pagedResults.Total=jobList.Count;
            return Ok(pagedResults);
        }





    }
}
