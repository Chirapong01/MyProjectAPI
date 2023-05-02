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

    public class SellController : ControllerBase
    {
        private readonly SellService _sellService;
        public SellController(SellService sellService)
        {
            _sellService = sellService;
        }

        [HttpGet]
        public ActionResult<List<Sell>> GetAllSell() => _sellService.GetSellAll();

        [HttpGet("{id}")]
        public ActionResult<Sell> GetByidSell(string id)
        {
            var sell = _sellService.GetSellByid(id);
            if (sell == null)
            {
                return NotFound();
            }
            return sell;
        }

        [HttpGet("{datex}")]
        public ActionResult<List<Sell>> GetSellByDate(DateTime datex)
        {
            var filter = _sellService.FilterSellBydate(datex);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Sell>> GetSellByRangeDate(DateTime datex1, DateTime datex2)
        {
            var filter = _sellService.FilterSellByRangeDate(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }

            return filter;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Sell>> GetSellByMonth(DateTime datex1, DateTime datex2)
        {
            var filter = _sellService.FilterSellByMonth(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }
            return filter;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Sell>> GetSellByYear(DateTime datex1, DateTime datex2)
        {
            var filter = _sellService.FilterSellByYear(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }
            return filter;
        }

        [HttpPost]
        public Sell AddSell([FromBody] Sell sell)
        {
            var i = _sellService.GetSellAllSkipClose();
            int j = i.Count() + 1;
            string fontName = sell.SellId.Substring(0, 3);
            sell.SellId = fontName + DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year + "0" + j.ToString();
            _sellService.CreateSell(sell);
            return sell;
        }
    } 

}