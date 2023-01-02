using JobManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobManagement.Controllers
{
    public class LocationsController : ApiController
    {
        JobManagementEntities db = new JobManagementEntities();

        //Create API -Post
        [HttpPost]
        public IHttpActionResult CreateLocation([FromBody] Location location)
        {
            Location loc = new Location();
            loc.Title = location.Title;
            loc.City = location.City;
            loc.State = location.State;
            loc.Country = location.Country;
            loc.ZipCode = location.ZipCode;
            db.Locations.Add(loc);
            db.SaveChanges();
            return Ok();
        }
        //Update API -PUT

        public IHttpActionResult UpdateLocation(int id, [FromBody] Location location)
        {
            var entity = db.Locations.FirstOrDefault(t => t.Id == id);
            if (entity == null)
            {
                BadRequest("Invalid Id");
            }
            entity.Title = location.Title;
            entity.City = location.City; 
            entity.State = location.State;
            entity.Country = location.Country;
            entity.ZipCode = location.ZipCode;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();

        }

        //Details API -Get
        [HttpGet]
        public IHttpActionResult GetLocations()
        {

            List<Location> LocationList = db.Locations.ToList();

            return Ok(LocationList);
        }
    }
}
