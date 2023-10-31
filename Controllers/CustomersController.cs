using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boamba_Emilia_Lab2.Data;
using Boamba_Emilia_Lab2.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using LibraryModel.Models;
using Microsoft.AspNetCore.Authorization;

namespace Boamba_Emilia_Lab2.Controllers
{
    [Authorize(Policy = "SalesManager")]
    public class CustomersController : Controller
    {
        private readonly LibraryContext _context;
        private string _baseUrl = "http://localhost:5295/api/Customers";
        private string _baseUrlCities = "http://localhost:5295/api/Cities";


        public CustomersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(await response.Content.ReadAsStringAsync());
                return View(customers);
            }
            return NotFound();

        }

        // GET: Inventory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/dto/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<CustomerDTO>(
                await response.Content.ReadAsStringAsync());
                return View(customer);
            }
            return NotFound();
        }

        // GET: Customers/Create
        public async Task<ActionResult> Create()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrlCities);
            if (response.IsSuccessStatusCode)
            {
                var cities = JsonConvert.DeserializeObject<List<City>>(await response.Content.ReadAsStringAsync());
                ViewData["CityID"] = new SelectList(cities, "ID", "CityName");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("CustomerID,Name,CityID,Adress,BirthDate")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: { ex.Message } ");
            }
            return View(customer);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                await response.Content.ReadAsStringAsync());
                response = await client.GetAsync(_baseUrlCities);
                if (response.IsSuccessStatusCode)
                {
                    var cities = JsonConvert.DeserializeObject<List<City>>(await response.Content.ReadAsStringAsync());
                    ViewData["CityID"] = new SelectList(cities, "ID", "CityName");
                }
                return View(customer);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("CustomerID,Name,CityID,Adress,BirthDate")] Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);
            var response = await client.PutAsync($"{_baseUrl}/{customer.CustomerID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/dto/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<CustomerDTO>(await response.Content.ReadAsStringAsync());
                return View(customer);
            }
            return new NotFoundResult();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("CustomerID")] CustomerDTO customer)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{customer.CustomerID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: { ex.Message} ");
            }
            return View(customer);
        }
    }
}

