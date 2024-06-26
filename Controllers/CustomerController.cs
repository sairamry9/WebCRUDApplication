using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebCRUDApplication.Models;

namespace WebCRUDApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("https://localhost:44380/api/") };
        }

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Customer");
            var jsonData = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(jsonData);
            return View(customers);
        }
                
        public async Task<ActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Customer/{id}");
            var jsonData = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerModel>(jsonData);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
                
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(customer);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Customers", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }
                        
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Customers/{id}");
            var jsonData = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerController>(jsonData);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
                
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(customer);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Customers/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }
                
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Customer/{id}");
            var jsonData = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerModel>(jsonData);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
                
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

    }
}