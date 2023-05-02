using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3MeePOSapi.Services
{
    public class SellService
    {
        private readonly IMongoCollection<Sell> _sell;
        private readonly IMongoCollection<Product> _product;
        public SellService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _sell = database.GetCollection<Sell>(settings.SellCollection);
            _product = database.GetCollection<Product>(settings.ProductCollection);
        }

        public List<Sell> GetSellAll() => _sell.Find(sell => sell.Status == "Open").ToList();
        public List<Sell> GetSellAllSkipClose() => _sell.Find(sell => true).ToList();
        public Sell GetSellByid(string id) => _sell.Find<Sell>(sell => sell.SellId == id.ToString()).FirstOrDefault();

        public List<Sell> FilterSellBydate(DateTime datex)
        {
            List<Sell> data = _sell.Find(s => s.CreationDatetime >= datex & s.CreationDatetime < datex.AddDays(+1)).ToList();
            List<Sell> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }

        public List<Sell> FilterSellByRangeDate(DateTime datex1, DateTime datex2)
        {
            List<Sell> data = _sell.Find(s => s.CreationDatetime >= datex1 & s.CreationDatetime < datex2.AddDays(+1)).ToList();
            List<Sell> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }
        public List<Sell> FilterSellByMonth(DateTime datex1, DateTime datex2)
        {
             List<Sell> data = _sell.Find(s => s.CreationDatetime >= datex1 & s.CreationDatetime < datex2.AddDays(+1)).ToList();
            List<Sell> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }

        public List<Sell> FilterSellByYear(DateTime datex1, DateTime datex2)
        {
           List<Sell> data = _sell.Find(s => s.CreationDatetime >= datex1 & s.CreationDatetime < datex2.AddYears(+1)).ToList();
            List<Sell> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }
        public Sell CreateSell(Sell sell)
        {
            foreach (var item in sell.ListSellItem)
            {
                var filterDefinition = Builders<Product>.Filter.Eq(p => p.ProductId, item.ProductId);
                var getProductByid = _product.Find<Product>(p => p.ProductId == item.ProductId).FirstOrDefault();
                int total = getProductByid.Balance - item.Amount;
                var updateDefinition = Builders<Product>.Update
                .Set(p => p.Balance, total);
                _product.UpdateOne(filterDefinition, updateDefinition);
            }
            _sell.InsertOne(sell);
            return sell;
        }
    }
}