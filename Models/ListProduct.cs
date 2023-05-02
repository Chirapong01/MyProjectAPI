using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class ListProduct
    {
        [BsonId]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// ราคาเฉลี่ยต่อชิ้น
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public int TotalPrice { get; set; }
    }

}