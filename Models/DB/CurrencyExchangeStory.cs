using System;
using System.Collections.Generic;

namespace UserApp.Models.DB
{
    public partial class CurrencyExchangeStory
    {
        public int Id { get; set; }
        public double? Rate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? ExchangeId { get; set; }
    }
}
