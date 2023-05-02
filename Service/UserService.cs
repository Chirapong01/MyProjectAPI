using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;

namespace _3MeePOSapi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UserCollection);
        }


        public List<User> GetUserAll() => _user.Find(user => user.Status == "Open").ToList();

        public User GetUserByid(string id) => _user.Find<User>(user => user.UserId == id).FirstOrDefault();

        public User GetUserByUsername(string username) => _user.Find<User>(user => (user.UserName == username) && (user.Status == "Open")).FirstOrDefault();

        public User CreateUser(User user)
        {
            _user.InsertOne(user);
            return user;
        }


        public void UpdateUser(string id, User userIn) => _user.ReplaceOne(user => user.UserId == id, userIn);

        public void DeletedUser(string id, User userIn) => _user.ReplaceOne(user => user.UserId == id, userIn);

        public User CheckUser(string username) => _user.Find<User>(user => user.UserName == username).FirstOrDefault();

        public User CheckUserAndPass(string username, string password) => _user.Find<User>(user => user.UserName == username && user.Password == password && user.Status == "Open").FirstOrDefault();
    }
}