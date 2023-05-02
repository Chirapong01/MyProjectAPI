using System;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class Sell
    {
        [BsonId]
        public string SellId { get; set; }
        /// <summary>
        /// ชื่อราคา
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ชื่อร้านค้าสมาชิก
        /// </summary>
        public string CustomerStoreName { get; set; }
         /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string TaxId { get; set; }
         /// <summary>
        /// ราคาสุทธิ
        /// </summary>
        public double NetPrice { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// ส่วนลดเปอร์เซ็น
        /// </summary>
        public int PerDiscount { get; set; }
        /// <summary>
        /// ภาษีมูลค่าเพิ่ม
        /// </summary>
        public int Vat { get; set; }
        /// <summary>
        /// รับเงิน
        /// </summary>
        public int ReceiveMoney { get; set; }
        /// <summary>
        /// เงินทอน
        /// </summary>
        public int ChangeMoney { get; set; }
        /// <summary>
        /// รายการสินค้าที่ขาย
        /// </summary>
        public ListSellItem[] ListSellItem { get; set; }
        public string Status { get; set; }
        public DateTime? CreationDatetime { get; set; }
        

    }
}