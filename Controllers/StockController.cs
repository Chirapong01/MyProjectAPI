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
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;
        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public ActionResult<List<Stock>> GetAllStock() => _stockService.GetStockAll();

        [HttpGet("{id}")]
        public ActionResult<Stock> GetStockByid(string id)
        {
            var stock = _stockService.GetStockByid(id);
            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpGet("{datex}")]
        public ActionResult<List<Stock>> GetStockByDate(DateTime datex)
        {
            var filter = _stockService.FilterStockBydate(datex);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Stock>> GetStockByRangeDate(DateTime datex1, DateTime datex2)
        {
            var filter = _stockService.FilterStockByRangeDate(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }

        [HttpPost]
        public Stock AddStock([FromBody] Stock stock)
        {
            stock.CreationDatetime = DateTime.Now;
            _stockService.CreateStock(stock);
            return stock;
        }

        [HttpPut("{id}")]
        public IActionResult EditStock([FromBody] Stock stock, string id)
        {
            var stocks = _stockService.GetStockByid(id);
            if (stocks == null)
            {
                return NotFound();
            }
            stock.StockId = id;
            _stockService.UpdateStock(id, stock);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeletedStock(string id)
        {
            var stocks = _stockService.GetStockByid(id);
            var statuschange = stocks.Status;
            if (stocks == null)
            {
                return NotFound();
            }
            if (statuschange == "Open")
            {
                statuschange = "Close";
            }
            stocks.Status = statuschange;
            _stockService.DeletedStock(id, stocks);
            return NoContent();
        }
    }
}