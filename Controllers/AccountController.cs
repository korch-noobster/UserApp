using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UserApp.Models;
using UserApp.Repository;
using UserApp.Services;

namespace UserApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[action]")]
    public class AccountController : Controller
    {
        private readonly SecurityServices _securityServices;
        private readonly DatabaseServices _databaseServices;
        private readonly DataServices _dataServices;
        private readonly RedisRepository _redisRepository;

        public AccountController(IConfiguration configuration, RedisRepository redisRepository)
        {
            _securityServices = new SecurityServices(configuration);
            _databaseServices = new DatabaseServices(configuration);
            _dataServices = new DataServices();
            _redisRepository = redisRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        // POST: api/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AccountModel userData)
        {
            var hashedPass = _securityServices.HashingPassword(userData.Password);
            _databaseServices.SetUser(userData.Username, hashedPass);
            var key = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 20);
            _databaseServices.SetKey(userData.Username, key);
            var userToken = _securityServices.GenerateJwtToken(userData.Username, key);
            await _redisRepository.Set(key, userToken.AccesToken);
            return Ok(userToken);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountModel userData)
        {
            var currentUser = _databaseServices.GetUser(userData.Username);
            if (currentUser == null)
            {
                return BadRequest("User is not set.");
            }

            if (!_securityServices.ValidatePassword(userData.Password, currentUser.PassHash))
            {
                return BadRequest("Wrong password");
            }

            var userToken = new ResultToken
            {
                AccesToken = await _redisRepository.Get(currentUser.TokenKey)
            };
            return Ok(userToken);
        }


        [HttpGet]
        public async Task<IActionResult> GetDemoRate([FromQuery]string from) //([FromBody] ExchangeModel exchangeData)
        {
            var exchangeData = new ExchangeModel
            {
                FromCurr = from,
                ToCurr = "USD"
            };
            var dbData = _databaseServices.GetDemoData(exchangeData);
            var kek = _dataServices.formatData(dbData);
            return new JsonResult(kek);
        }

    }
}

