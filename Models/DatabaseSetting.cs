namespace _3MeePOSapi.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollection { get; set; }
        public string CustomerCollection { get; set; }
        public string ProductCollection { get; set; }
        public string StockCollection { get; set; }
        public string SellCollection { get; set; }
    }

    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollection { get; set; }
        string CustomerCollection { get; set; }
        string ProductCollection { get; set; }
        string StockCollection { get; set; }
        string SellCollection { get; set; }
    }
}