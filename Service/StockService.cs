using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3MeePOSapi.Services
{
    public class StockService
    {
        private readonly IMongoCollection<Stock> _stock;
        private readonly IMongoCollection<Product> _product;

        public StockService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _stock = database.GetCollection<Stock>(settings.StockCollection);
            _product = database.GetCollection<Product>(settings.ProductCollection);
        }

        public List<Stock> GetStockAll() => _stock.Find(stock => stock.Status == "Open").ToList();

        public Stock GetStockByid(string id) => _stock.Find<Stock>(stock => stock.StockId == id).FirstOrDefault();

        public List<Stock> FilterStockBydate(DateTime datex)
        {
            List<Stock> data = _stock.Find(s => s.CreationDatetime >= datex & s.CreationDatetime < datex.AddDays(+1)).ToList();
            List<Stock> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }

        public List<Stock> FilterStockByRangeDate(DateTime datex1, DateTime datex2)
        {
            List<Stock> data = _stock.Find(s => s.CreationDatetime >= datex1 & s.CreationDatetime < datex2.AddDays(+1)).ToList();
            List<Stock> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }
        public Stock CreateStock(Stock stock)
        {
            foreach (var item in stock.ListProduct)
            {
                var filterDefinition = Builders<Product>.Filter.Eq(p => p.ProductId, item.ProductId);
                var getProductByid = _product.Find<Product>(p => p.ProductId == item.ProductId).FirstOrDefault();
                int counter = getProductByid.counterProduct;
                int total = getProductByid.Balance + item.Amount;
                int costavg = (((getProductByid.CostAvg * counter) + (item.price * item.Amount)) / (counter + item.Amount));
                var updateDefinition = Builders<Product>.Update
                .Set(p => p.Balance, total)
                .Set(p => p.CostAvg, costavg)
                .Set(p => p.CostNew, item.price)
                .Set(p => p.counterProduct, counter + item.Amount);
                _product.UpdateOne(filterDefinition, updateDefinition);
            }
            _stock.InsertOne(stock);
            return stock;
        }

        public void UpdateStock(string id, Stock stockIn) => _stock.ReplaceOne(stock => stock.StockId == id, stockIn);

        public void DeletedStock(string id, Stock stockIn) => _stock.ReplaceOne(stock => stock.StockId == id, stockIn);
    }
}