using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobManagement.Models
{
    public class ModelClass
    {
        public class JobOpeningView
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int LocationId { get; set; }
            public int DepartmentId { get; set; }
            public DateTime ClosingDate { get; set; }
        }
        public class JobOpeningDetails
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Location Location { get; set; }
            public Department Department { get; set; }
            public DateTime PostedDate { get; set; }
            public DateTime ClosingDate { get; set; }
        }

        public class PagedSearchRequest
        {
            public string q { get; set; }
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public int? LocationId { get; set; }
            public int? DepartmentId { get; set; }
        }

        public class Job
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public string Department { get; set; }
            public DateTime PostedDate { get; set; }
            public DateTime ClosingDate { get; set; }
        }
        public class PagedResults
        {
            public int Total { get; set; }
            public List<Job> Data { get; set; }
        }
    }
}