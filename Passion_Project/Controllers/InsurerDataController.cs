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

namespace Passion_Project.Controllers
{
    public class InsurerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/InsurerData/ListInsurers
        [HttpGet]
        public IQueryable<Insurer> ListInsurers()
        {
            return db.Insurers;
        }

        // GET: api/InsurerData/FindInsurer/5
        [ResponseType(typeof(Insurer))]
        [HttpGet]
        public IHttpActionResult FindInsurer(int id)
        {
            Insurer insurer = db.Insurers.Find(id);
            if (insurer == null)
            {
                return NotFound();
            }

            return Ok(insurer);
        }

        // POST: api/InsurerData/UpdateInsurer5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateInsurer(int id, Insurer insurer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insurer.InsurerID)
            {
                return BadRequest();
            }

            db.Entry(insurer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurerExists(id))
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

        // POST: api/InsurerData/AddInsurer
        [ResponseType(typeof(Insurer))]
        [HttpPost]
        public IHttpActionResult AddInsurer(Insurer insurer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Insurers.Add(insurer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = insurer.InsurerID }, insurer);
        }

        // POST: api/InsurerData/DeleteInsurer/5
        [ResponseType(typeof(Insurer))]
        [HttpPost]
        public IHttpActionResult DeleteInsurer(int id)
        {
            Insurer insurer = db.Insurers.Find(id);
            if (insurer == null)
            {
                return NotFound();
            }

            db.Insurers.Remove(insurer);
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

        private bool InsurerExists(int id)
        {
            return db.Insurers.Count(e => e.InsurerID == id) > 0;
        }
    }
}