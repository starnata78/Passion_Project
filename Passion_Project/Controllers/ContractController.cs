 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Passion_Project.Models;
using Passion_Project.Models.ViewModels;
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
            client.BaseAddress = new Uri("https://localhost:44345/api/");
        }
        // GET: Contract/List
        public ActionResult List()
        {
            //communicate with contract data api to retrieve a list of contracts
            //curl https://localhost:44345/api/contractdata/listcontracts

            string url = "contractdata/listcontracts";
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

            string url = "contractdata/findcontract/" + id;
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
            ContractNew ViewModel = new ContractNew();

            //information about all owners, policies and insurers in the system to choose from

            string url = "ownerdata/listowners";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Owner> ownersOptions = response.Content.ReadAsAsync<IEnumerable<Owner>>().Result;
            ViewModel.AvailableOwners = ownersOptions;
            //return View(ownersOptions);

            url = "PolicyData/ListPolicies";
            response = client.GetAsync(url).Result;
            IEnumerable<Policy> AvailablePolicies = response.Content.ReadAsAsync<IEnumerable<Policy>>().Result;
            ViewModel.AvailablePolicies = AvailablePolicies;

            url = "InsurerData/ListInsurers";
            response = client.GetAsync(url).Result;
            IEnumerable<Insurer> AvailableInsurers = response.Content.ReadAsAsync<IEnumerable<Insurer>>().Result;
            ViewModel.AvailableInsurers = AvailableInsurers;

            return View(ViewModel);


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
            string url = "contractdata/addcontract";

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

        // GET: Contract/Update/5
        public ActionResult Update(int id)
        {
            UpdateContract ViewModel = new UpdateContract();

          /*View model the existing contract information to be included to accommodate
           * upgate requrest of ownder ID. In order to achieve it we'll create a class to 
           * represent both of pieces of information: all owners and all contracts to choose from
           * path: Model folder - Add - New Folder - call it "ViewModels" - inside "ViewModels" - add - Class - call it "UpdateContgract"
           */

            //find the contract to show to the user so they know what to edit
            string url = "contractdata/findcontract/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ContractDto SelectedContract = response.Content.ReadAsAsync<ContractDto>().Result;
            ViewModel.SelectedContract = SelectedContract;

            // also like to include all owners id to choose from when updating a particular contract
            // In case owner id was put incorrectly when creating a new contract. So instead of deleting the 
            //the entire record, we can amend (update ownder id).

            url = "contractdata/listowners/";
            response = client.GetAsync(url).Result;
            IEnumerable<Owner> ownerOptions = response.Content.ReadAsAsync<IEnumerable<Owner>>().Result;

            ViewModel.ownersOptions = ownerOptions;

            return View(ViewModel);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Contract contract)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(contract.ID);
        //Objective:update an existing contract in the system using API
        //curl -d contract.json -H "Content-type:application/json "https://localhost:44345/api/contractdata/updatecontract/5"
        //POST: api / ContractData / UpdateContract / 5
            string url = "contractdata/updatecontract/"+id;

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
                return RedirectToAction("Error");
            }
        }
        
        // GET: Contract/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "contractdata/findcontract/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            ContractDto selectedcontract = response.Content.ReadAsAsync<ContractDto>().Result;
            //Debug.WriteLine("Contract received:");
            //Debug.WriteLine(seclectedcontract.id);

            return View(selectedcontract);

        }


        // POST: Contract/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //curl/api/contractdata/deletecontract -d ""
            string url = "contractdata/deletecontract/" + id;
            string payload = "";

            HttpContent content = new StringContent(payload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;


            return RedirectToAction("List");
        }
    }
}
