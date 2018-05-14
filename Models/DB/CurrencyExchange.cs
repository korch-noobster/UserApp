using System;
using System.Collections.Generic;

namespace UserApp.Models.DB
{
    public partial class CurrencyExchange
    {
        public int Id { get; set; }
        public int? FromCurr { get; set; }
        public int? ToCurr { get; set; }
        public string Abbreviation { get; set; }
    }
}
