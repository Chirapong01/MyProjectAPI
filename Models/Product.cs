using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class Product
    {
        [BsonId]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// ยี่ห้อ
        /// </summary>
        public string Brand { get; set; }
         /// <summary>
        /// รุ่น
        /// </summary>
        public string Model { get; set; }
         /// <summary>
        /// ประเภทสินค้า
        /// </summary>
        public string Type { get; set; }
         /// <summary>
        /// ต้นทุนล่าสุด
        /// </summary>
        public int CostNew { get; set; }
         /// <summary>
        /// ต้นทุนเฉลี่ย
        /// </summary>
        public int CostAvg { get; set; }
         /// <summary>
        /// ราคาปลีก
        /// </summary>
        public int Price1 { get; set; }
         /// <summary>
        /// ราคาสมาชิก
        /// </summary>
        public int Price2 { get; set; }
         /// <summary>
        /// ราคาส่ง
        /// </summary>
        public int Price3 { get; set; }
         /// <summary>
        /// จำนวนที่ต้องเฉลี่ย
        /// </summary>
        public int counterProduct { get; set;}
         /// <summary>
        /// จำนวนคงเหลือ
        /// </summary>
        public int Balance { get; set; }
        public string Status { get; set; }
         /// <summary>
        /// คนอัตเดตข้อมูล
        /// </summary>
        public string UserUpdate { get; set; }
         /// <summary>
        /// วันที่อัพเดตข้อมูลล่าสุด
        /// </summary>
        public DateTime? UpdationDatetime { get; set; }
        public DateTime? CreationDatetime { get; set; }
    }
}