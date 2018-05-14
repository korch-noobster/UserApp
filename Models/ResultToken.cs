using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UserApp.Models
{
    public class ResultToken
    {
        [DataMember]
        public string AccesToken { get; set; }
    }
}
