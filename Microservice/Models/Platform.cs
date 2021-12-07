using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }




    }
}
