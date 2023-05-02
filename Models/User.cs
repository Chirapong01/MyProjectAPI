using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserTel { get; set; }
        /// <summary>
        /// ระดับสิทธิ์
        /// </summary>
        public string Rank { get; set; }
        public string Status { get; set; }
    }
}