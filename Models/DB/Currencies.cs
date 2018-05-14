using System;
using System.Collections.Generic;

namespace UserApp.Models.DB
{
    public partial class Currencies
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
