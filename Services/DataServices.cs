using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Models;
using UserApp.Models.DB;

namespace UserApp.Services
{
    public class DataServices
    {
        public List<DemoDataOutputModel> formatData(IEnumerable<CurrencyExchangeStory> data)
        {
            var output = new List<DemoDataOutputModel>();
            foreach (var item in data)
            {
                output.Add(new DemoDataOutputModel
                {
                    Timestamp = (long) (item.UpdateDate.GetValueOrDefault(DateTime.UtcNow) - new DateTime(1970, 1, 1))
                        .TotalMilliseconds,
                    Value = item.Rate.GetValueOrDefault()
                });
            }

            return output;
        }
    }
}

