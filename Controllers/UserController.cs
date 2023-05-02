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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUser() => _userService.GetUserAll();

        [HttpGet("{id}")]
        public ActionResult<User> GetUserByid(string id)
        {
            var user = _userService.GetUserByid(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

     
        [HttpGet("{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public User AddUser([FromBody] User user)
        {
            _userService.CreateUser(user);
            return user;
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody] User user, string id)
        {
            var users = _userService.GetUserByid(id);
            if (users == null)
            {
                return NotFound();
            }
            user.UserId = id;
            _userService.UpdateUser(id, user);
            return NoContent();
        }



        [HttpGet("{id}")]
        public IActionResult DeletedUser(string id)
        {
            var users = _userService.GetUserByid(id);
            var statuschange = users.Status;
            if (users == null)
            {
                return NotFound();
            }
            if (statuschange == "Open")
            {
                statuschange = "Close";
            }
            users.Status = statuschange;
            _userService.DeletedUser(id, users);
            return NoContent();
        }

        [HttpGet("{username}")]
        public string CheckUser(string username)
        {
            var Username = _userService.CheckUser(username);
            if (Username == null)
            {
                return "ชื่อนี้สามารถใช้งานได้";
            }

            return "ชื่อนี้มีในระบบแล้ว";
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<User> CheckUserAndPass(string username, string password)
        {
            var userAndPass = _userService.CheckUserAndPass(username, password);
            if (userAndPass == null)
            {
                return NotFound();
            }
            return userAndPass;
        }
    }
}