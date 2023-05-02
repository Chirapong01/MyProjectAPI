using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using _3MeePOSapi.Models;
using _3MeePOSapi.Services;

namespace _3MeePOSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAllCustomer() => _customerService.GetCustomerAll();

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerByid(string id)
        {
            var customer = _customerService.GetCustomerByid(id);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpGet("{datex}")]
        public ActionResult<List<Customer>> GetCustomerByDate(DateTime datex)
        {
            var customer = _customerService.FilterCustomerBydate(datex);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Customer>> GetCustomerByRangeDate(DateTime datex1 ,DateTime datex2)
        {
            var customer = _customerService.FilterCustomerByRangeDate(datex1, datex2);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public Customer AddCustomer([FromBody] Customer customer)
        {
            _customerService.CreateCustomer(customer);
            return customer;
        }

        [HttpPut("{id}")]
        public IActionResult EditCustomer([FromBody] Customer customer, string id)
        {
            var customers = _customerService.GetCustomerByid(id);
            if (customers == null)
            {
                return NotFound();
            }
            customer.CustomerId = id;
            _customerService.UpdateCustomer(id, customer);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeletedCustomer(string id)
        {
            var customers = _customerService.GetCustomerByid(id);
            var statuschange = customers.Status;
            if (customers == null)
            {
                return NotFound();
            }
            if (statuschange == "Open")
            {
                statuschange = "Close";
            }
            customers.Status = statuschange;
            _customerService.DeletedCustomer(id, customers);
            return NoContent();
        }
    }
}