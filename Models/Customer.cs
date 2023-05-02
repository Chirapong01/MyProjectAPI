using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class Customer
    {
        [BsonId]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        /// <summary>
        /// ชื่อร้านลูกค้า
        /// </summary>
        public string CustomerStoreName { get; set; }
        public string Address { get; set; }
        public string CustomerTel { get; set; }
        /// <summary>
        /// คะแนน
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// ประเภทลูกค้า
        /// </summary>
        public string Type { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime? CreationDatetime { get; set; }
    }
}