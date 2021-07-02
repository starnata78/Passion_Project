using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Passion_Project.Models;
using System.Diagnostics;

namespace Passion_Project.Controllers
{
    public class PolicyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

     
        // GET: api/PolicyData/ListPolicies
        [HttpGet]

        public IQueryable<Policy> ListPolicies()
        {
            return db.Policies;
        }


        // GET: api/PolicyData/FindPolicy/5
        [ResponseType(typeof(Policy))]
        [HttpGet]
        public IHttpActionResult FindPolicy(int id)
        {
            Policy policy = db.Policies.Find(id);

            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        // POST: api/PolicyData/UpdatePolicy/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.PolicyID)
            {
                return BadRequest();
            }

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PolicyData/AddAnimal
        [ResponseType(typeof(Policy))]
        [HttpPost]
        public IHttpActionResult AddPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Policies.Add(policy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policy.PolicyID }, policy);
        }

        // POST: api/PolicyData/DeletePolicy/5
        [ResponseType(typeof(Policy))]
        [HttpPost]
        public IHttpActionResult DeletePolicy(int id)
        {
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return NotFound();
            }

            db.Policies.Remove(policy);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PolicyExists(int id)
        {
            return db.Policies.Count(e => e.PolicyID == id) > 0;
        }
    }
}