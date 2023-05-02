using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class Stock
    {
        /// <summary>
        /// รหัสใบรับ/ส่งสินค้า
        /// </summary>
        [BsonId]
        public string StockId { get; set; }
        /// <summary>
        /// ชื่อบริษัท
        /// </summary>
        public string BillProduct { get; set; }
        public string Status { get; set; }
        public DateTime? CreationDatetime { get; set; }
         /// <summary>
        /// รายการสินค้า
        /// </summary>
        public ListProduct[] ListProduct { get; set; }
    }

}