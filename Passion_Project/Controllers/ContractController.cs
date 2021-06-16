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

            IEnumerable<ContractDto> contracts = response.Content.ReadAsAsync<IEnumerable<ContractDto>>().Result;
            Debug.WriteLine("Number of contracts received:");
            Debug.WriteLine(contracts.Count());


            return View(contracts);
        }


        // GET: Contract/Details/5
        public ActionResult Details(int id)
        { //communicate with contract data api to retrieve an contract
            //curl https://localhost:44345/api/contractdata/findcontract/id

            string url = "findcontract/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            ContractDto selectedcontract = response.Content.ReadAsAsync<ContractDto>().Result;
            //Debug.WriteLine("Contract received:");
            //Debug.WriteLine(seclectedcontract.id);

            return View(selectedcontract);

        }
        public ActionResult Error()
        {
            return View();
        }

        // GET: Contract/New
        //This method asks the user for information about a contract
        public ActionResult New()
        {
            return View();
        }


        //This methods is responsible for creating a contract on itself
        // POST: Contract/Create
        [HttpPost]
        public ActionResult Create(Contract contract)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(contract.ID);
            //Objective:add new contract into the system using API
            //curl -d contract.json -H "Content-type:application/json "https://localhost:44345/api/contractdata/addcontract"
            string url = "addcontract";

            string jsonpayload = jss.Serialize(contract);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
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
