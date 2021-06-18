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
    public class OwnerController : Controller
    {
        //factoring code

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static OwnerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/api/ownerdata/");
        }

        // GET: Owner/List
        public ActionResult List()
        {
            //communicate with owner data api to retrieve a list of owners
            //curl https://localhost:44345/api/ownerdata/listowners

            string url = "listowners";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Owner> owners = response.Content.ReadAsAsync<IEnumerable<Owner>>().Result;
            //Debug.WriteLine("Number of owners received:");
            //Debug.WriteLine(owners.Count());


            return View(owners);
        }

        // GET: Owner/Details/5
        public ActionResult Details(int id)
        {
            //communicate with owner data api to retrieve an owner
            //curl https://localhost:44345/api/ownerdata/findowner/id

            string url = "findowner/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            Owner selectedowner = response.Content.ReadAsAsync<Owner>().Result;
            //Debug.WriteLine("Owners received:");
            //Debug.WriteLine(selectedowner.First_Name);

            return View(selectedowner);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Owner/New
        //This method asks the user for information about an owner
        public ActionResult New()
        {
            return View();
        }

        //This methods is responsible for creating an owner on itself
        // POST: Owner/Create
        [HttpPost]
        public ActionResult Create(Owner owner)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(owner.First_Name);
            //Objective:add new owner into the system using API
            //curl -d owner.json -H "Content-type:application/json "https://localhost:44345/api/ownerdata/addowner"
            string url = "addowner";

            string jsonpayload = jss.Serialize(owner);

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

        // GET: Owner/Update/5
        public ActionResult Update(int id)
        {
            //find the owner to show the user so they know what to update

            string url = "findowner/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Owner selectedowner = response.Content.ReadAsAsync<Owner>().Result;
            
            return View(selectedowner);
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Owner owner)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(owner.First_Name);
            //Objective:edit an existing owner in the system using API
            //curl -d owner.json -H "Content-type:application/json "https://localhost:44345/api/ownerdata/addowner/5"
            string url = "updateowner/"+id;
            string jsonpayload = jss.Serialize(owner);

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

        // GET: Owner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Owner/Delete/5
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
