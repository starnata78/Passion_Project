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
    public class PolicyController : Controller
    {
        //factoring code

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PolicyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/api/policydata/");
        }

        // GET: Policy/List
        public ActionResult List()
        {
            //communicate with policy data api to retrieve a list of policies
            //curl https://localhost:44345/api/policydata/listpolicies

            string url = "listpolicies";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Policy> policies = response.Content.ReadAsAsync<IEnumerable<Policy>>().Result;
            //Debug.WriteLine("Number of policies received:");
            //Debug.WriteLine(owners.Count());

            return View(policies);
        }

        // GET: Policy/Details/5
        public ActionResult Details(int id)
        {
            //communicate with policy data api to retrieve a policy
            //curl https://localhost:44345/api/policydata/findpolicy/id

            string url = "findpolicy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            Policy selectedpolicy = response.Content.ReadAsAsync<Policy>().Result;
            //Debug.WriteLine("Policy received:");
            //Debug.WriteLine(selectedpolicy.Name);

            return View(selectedpolicy);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Policy/New
        //This method asks the user for information about an policy
        public ActionResult New()
        {
            return View();
        }

        //This methods is responsible for creating an owner on itself
        // POST: Policy/Create
        [HttpPost]
        public ActionResult Create(Policy policy)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(policy.Name);
            //Objective:add new policy into the system using API
            //curl -d owner.json -H "Content-type:application/json "https://localhost:44345/api/policydata/addpolicy"
            string url = "addpolicy";

            string jsonpayload = jss.Serialize(policy);

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

        // GET: Policy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Policy/Edit/5
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

        // GET: Policy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Policy/Delete/5
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
