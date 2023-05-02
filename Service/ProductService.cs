using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3MeePOSapi.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            var filter = Builders<Product>.Filter.Where(p => p.Status == "Open");

            _product = database.GetCollection<Product>(settings.ProductCollection); 
        }

        public List<Product> GetProductAll() => _product.Find(p => p.Status == "Open").ToList();
        public List<Product> GetProductByBrand(string brand)
        {
            var data = _product.Find<Product>(p => p.Status == "Open").ToList();
            var filter = data.Where(p => p.Brand.ToLower() == brand.ToLower()).ToList();
            return filter;
        }
        public List<Product> GetProductByType(string type)
        {
            var data = _product.Find<Product>(p => p.Status == "Open").ToList();
            var filter = data.Where(p => p.Type.ToLower() == type.ToLower()).ToList();
            return filter;
        }
        public Product GetProductByid(string id) => _product.Find<Product>(p => p.ProductId == id && p.Status == "Open").FirstOrDefault();

        public List<Product> FilterProductBydate(DateTime datex)
        {
            List<Product> data = _product.Find(p => p.CreationDatetime >= datex & p.CreationDatetime < datex.AddDays(+1)).ToList();
            List<Product> filterStatus = data.FindAll(pp => pp.Status == "Open").ToList();
            return filterStatus;
        }

        public List<Product> FilterProductByRangeDate(DateTime datex1, DateTime datex2)
        {
            List<Product> data = _product.Find(p => p.CreationDatetime >= datex1 & p.CreationDatetime < datex2.AddDays(+1)).ToList();
            List<Product> filterStatus = data.FindAll(pp => pp.Status == "Open").ToList();
            return filterStatus;
        }

        public Product CreateProduct(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public string UpdateProduct(string id, Product productIn) 
        {
            _product.ReplaceOne(p => p.ProductId == id, productIn);
            return "Update Succeess";
        } 

        public string DeletedProduct(string id, Product productIn)
        {
            _product.ReplaceOne(p => p.ProductId == id, productIn);
            return "Delete Succeess";
        }    

            
    }
}