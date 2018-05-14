
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using UserApp.Models;
using UserApp.Models.DB;

namespace UserApp.Services
{
    public class DatabaseServices
    {
        private readonly IConfiguration _configuration;
        public DatabaseServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int SetUser(string username, string passHash)
        {
            using (var connectionToTestDb = new SqlConnection(_configuration["DefaultConnection"]))
            {
                var result = connectionToTestDb
                    .Query<int>("INSERT INTO Users (Username,PassHash) VALUES(@username,@passHash);SELECT CAST(SCOPE_IDENTITY() as int)", new { username, passHash }).SingleOrDefault();
                return result;
            }
        }
        public int SetKey(string username, string key)
        {
            using (var connectionToTestDb = new SqlConnection(_configuration["DefaultConnection"]))
            {
                var result = connectionToTestDb
                    .Query<int>("UPDATE Users SET TokenKey=@key WHERE Username=@username", new { key ,username }).SingleOrDefault();
                return result;
            }
        }
  
        public Users GetUser(string username)
        {
            using (var connectionToTestDb = new SqlConnection(_configuration["DefaultConnection"]))
            {
                var result = connectionToTestDb
                    .Query<Users>("SELECT * FROM Users WHERE Username=@username", new { username }).SingleOrDefault();
                return result;
            }
        }

        public IEnumerable<CurrencyExchangeStory> GetDemoData(ExchangeModel request)
        {
            using (var connectionToTestDb = new SqlConnection(_configuration["DefaultConnection"]))
            {
                var from = connectionToTestDb.Query<int>("SELECT Id FROM Currencies WHERE Currency_code= @fromCurr",
                    new { fromCurr = request.FromCurr.Trim() }).SingleOrDefault();
                var to = connectionToTestDb.Query<int>("SELECT Id FROM Currencies WHERE Currency_code= @toCurr",
                    new { toCurr = request.ToCurr }).SingleOrDefault();
                var exId = connectionToTestDb.Query<int>("SELECT Id FROM CurrencyExchange WHERE FromCurr=@from AND ToCurr=@to",
                    new { from, to }).SingleOrDefault();
                var exchangeData = connectionToTestDb.Query<CurrencyExchangeStory>("SELECT * FROM CurrencyExchangeStory WHERE ExchangeId= @exId",
                    new { exId });
                return exchangeData;
            }
        }
    }
}
