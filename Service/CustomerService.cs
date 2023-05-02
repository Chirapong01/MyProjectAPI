using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3MeePOSapi.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customer;

        public CustomerService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _customer = database.GetCollection<Customer>(settings.CustomerCollection);
        }

        public List<Customer> GetCustomerAll() => _customer.Find(customer => customer.Status == "Open").ToList();

        public Customer GetCustomerByid(string id) => _customer.Find<Customer>(customer => customer.CustomerId == id).FirstOrDefault();
        
        public List<Customer> GetCustomerByDate(DateTime date)
        {
            var fillerDate = Builders<Customer>.Filter.Eq(c => c.CreationDatetime, date);
            List<Customer> filterData = _customer.Find(fillerDate).ToList();
            List<Customer> data = filterData.FindAll(cc => cc.Status == "Open").ToList();
            return data.ToList();
        }

        public List<Customer> FilterCustomerBydate(DateTime datex)
        {
            List<Customer> data = _customer.Find(c => c.CreationDatetime >= datex & c.CreationDatetime < datex.AddDays(+1)).ToList();
            List<Customer> filterStatus = data.FindAll(cc => cc.Status == "Open").ToList();
            return filterStatus;
        }

        public List<Customer> FilterCustomerByRangeDate(DateTime datex1, DateTime datex2)
        {
            List<Customer> data = _customer.Find(c => c.CreationDatetime >= datex1 & c.CreationDatetime < datex2.AddDays(+1)).ToList();
            List<Customer> filterStatus = data.FindAll(cc => cc.Status == "Open").ToList();
            return filterStatus;
        }

        public Customer CreateCustomer(Customer customer)
        {
            customer.CreationDatetime = DateTime.Now.Date;
            _customer.InsertOne(customer);
            return customer;
        }

        public void UpdateCustomer(string id, Customer customerIn) => _customer.ReplaceOne(customer => customer.CustomerId == id, customerIn);

        public void DeletedCustomer(string id, Customer customerIn) => _customer.ReplaceOne(customer => customer.CustomerId == id, customerIn);
    }
}