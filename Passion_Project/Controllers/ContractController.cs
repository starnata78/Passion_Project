using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Passion_Project.Models;
using System.Web.Script.Serialization;

namespace Passion_Project.Controllers
{
    public class ContractController : Controller
    {
        //factoring code

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ContractController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/api/contractdata/");
        }
        // GET: Contract/List
        public ActionResult List()
        {
            //communicate with contract data api to retrieve a list of contracts
            //curl https://localhost:44345/api/contractdata/listcontracts

            string url = "listcontracts";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The resonse code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Contract> contracts = response.Content.ReadAsAsync<IEnumerable<Contract>>().Result;
            Debug.WriteLine("Number of contracts received:");
            Debug.WriteLine(contracts.Count());


            return View(contracts);
        }

        // GET: Contract/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contract/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contract/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contract/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contract/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contract/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
