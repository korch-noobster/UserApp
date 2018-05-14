using System;
using System.Collections.Generic;

namespace UserApp.Models.DB
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string TokenKey { get; set; }
    }
}
