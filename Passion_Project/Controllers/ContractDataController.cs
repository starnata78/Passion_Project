using Passion_Project.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Passion_Project.Controllers
{
    public class ContractDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ContractData/ListContracts
        [HttpGet]
        public IEnumerable<ContractDto> ListContracts()
        {
            List<Contract> Contracts = db.Contracts.ToList();
            List<ContractDto> ContractDtos = new List<ContractDto>();

            Contracts.ForEach(a => ContractDtos.Add(new ContractDto()
            {
                ID = a.ID,
                OwnerID = a.OwnerID,
                First_Name = a.Owner.First_Name,
                Last_Name = a.Owner.Last_Name,
                Policy = a.Policy.Name,
                Insurer = a.Insurer.Name,
                Active = a.Active,
                PurchaseDate = a.PurchaseDate
            }));

            return ContractDtos;
               
        }

        // GET: api/ContractData/FindContract/5
        [ResponseType(typeof(Contract))]
        [HttpGet]
        public IHttpActionResult FindContract(int id)
        {
            Contract Contract = db.Contracts.Find(id);
            ContractDto ContractDto = new ContractDto()
            {
                ID = Contract.ID,
                OwnerID = Contract.OwnerID,
                First_Name = Contract.Owner.First_Name,
                Last_Name = Contract.Owner.Last_Name,
                Policy = Contract.Policy.Name,
                PolicyId = Contract.PolicyID,
                Insurer = Contract.Insurer.Name,
                InsurerId = Contract.InsurerID,
                Active = Contract.Active,
                PurchaseDate = Contract.PurchaseDate

            };
            if (Contract == null)
            {
                return NotFound();
            }

            return Ok(ContractDto);
        }

        // POST: api/ContractData/UpdateContract/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateContract(int id, Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.ID)
            {
                return BadRequest();
            }

            db.Entry(contract).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/ContractData/AddContract
        [ResponseType(typeof(Contract))]
        [HttpPost]
        public IHttpActionResult AddContract(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contracts.Add(contract);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contract.ID }, contract);
        }

        // POST: api/ContractData/DeleteContract/5
        [ResponseType(typeof(Contract))]
        [HttpPost]
        public IHttpActionResult DeleteContract(int id)
        {
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return NotFound();
            }

            db.Contracts.Remove(contract);
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

        private bool ContractExists(int id)
        {
            return db.Contracts.Count(e => e.ID == id) > 0;
        }
    }
}