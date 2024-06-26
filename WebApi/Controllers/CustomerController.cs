using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static WebApi.Models.CustomerModel;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Customer1", Address = "London", PhoneNumber = "+987676" },
            new Customer { Id = 2, Name = "Customer2", Address = "Manchester", PhoneNumber = "+445654545" },
            new Customer { Id = 3, Name = "Customer3", Address = "Glasgow", PhoneNumber = "+44565788" }
        };
       
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return customers;
        }
               
        [HttpGet]
        public IHttpActionResult GetCustomers(int id)
        {
            var cust = customers.FirstOrDefault(p => p.Id == id);
            if (cust == null)
            {
                return NotFound();
            }
            return Ok(cust);
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid data.");
            }
            customer.Id = customers.Max(p => p.Id) + 1;
            customers.Add(customer);
            return Ok(customer);
        }

        
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid data.");
            }
            var existingCustomer = customers.FirstOrDefault(p => p.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(p => p.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customers.Remove(customer);
            return Ok();
        }
    }
}

