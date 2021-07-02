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
            client.BaseAddress = new Uri("https://localhost:44345/api/policydata");
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

            Policy SelectedPolicy = response.Content.ReadAsAsync<Policy>().Result;
            //Debug.WriteLine("Policy received:");
            //Debug.WriteLine(selectedpolicy.Name);

            return View(SelectedPolicy);
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

        // GET: Policy/Update/5
        public ActionResult Update(int id)
        {
            //find the policy to show the user so they know what to update

            string url = "findpolicy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Policy selectedpolicy = response.Content.ReadAsAsync<Policy>().Result;

            return View(selectedpolicy);
        }

        // POST: Policy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Policy policy)
        {

            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(policy.Name);
            //Objective:edit an existing policy in the system using API
            //curl -d owner.json -H "Content-type:application/json "https://localhost:44345/api/policydata/addpolicy/5"
            string url = "updatepolicy/" + id;
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
        
        // GET: Policy/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "findpolicy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("The resonse code is");
            Debug.WriteLine(response.StatusCode);

            Policy selectedpolicy = response.Content.ReadAsAsync<Policy>().Result;
            Debug.WriteLine("Contract received:");
            Debug.WriteLine(selectedpolicy.PolicyID);

            return View(selectedpolicy);
        }

        // POST: Policy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //curl /api/policydata/deletepolicy -d""
            string url = "deletepolicy/" + id;
            string payload = "";
            HttpContent content = new StringContent(payload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("List");
        }
    }
}
