using System.ComponentModel.DataAnnotations;

namespace UserApp.Models
{
    public class ExchangeModel
    {
        [Required]
        public string FromCurr { get; set; }
        [Required]
        public string ToCurr { get; set; }

    }
}
