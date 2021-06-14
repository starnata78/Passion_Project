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
    public class InsurerController : Controller
    {
        //factoring code

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static InsurerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/api/insurerdata/");
        }

        // GET: Insurer/List
        public ActionResult List()
        {
            //communicate with insurer data api to retrieve a list of insurers
            //curl https://localhost:44345/api/insurerdata/listinsurers

            string url = "listinsurers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Insurer> insurers = response.Content.ReadAsAsync<IEnumerable<Insurer>>().Result;
            //Debug.WriteLine("Number of insurers received:");
            //Debug.WriteLine(insurers.Count());


            return View(insurers);
        }

        // GET: Insurer/Details/5
        public ActionResult Details(int id)
        {
            //communicate with insurer data api to retrieve an insurer
            //curl https://localhost:44345/api/insurerdata/findinsurer/id

            string url = "findinsurer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The resonse code is");
            //Debug.WriteLine(response.StatusCode);

            Insurer selectedinsurer = response.Content.ReadAsAsync<Insurer>().Result;
            //Debug.WriteLine("Insurers received:");
            //Debug.WriteLine(selectedinsure.Name);

            return View(selectedinsurer);
        }
        public ActionResult Error()
        {
            return View();
        }

        // GET: Insurer/New
        //This method asks the user for information about an insurer
        public ActionResult New()
        {
            return View();
        }

        // POST: Insurer/Create
        //This methods is responsible for creating an insurer on itself
        [HttpPost]
        public ActionResult Create(Insurer insurer)
        {
            Debug.WriteLine("jsonpayload is: ");
            Debug.WriteLine(insurer.Name);
            //Objective:add new insurer into the system using API
            //curl -d owner.json -H "Content-type:application/json "https://localhost:44345/api/insurerdata/addinsurer"
            string url = "addinsurer";

            string jsonpayload = jss.Serialize(insurer);

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

        // GET: Insurer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Insurer/Edit/5
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

        // GET: Insurer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Insurer/Delete/5
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
