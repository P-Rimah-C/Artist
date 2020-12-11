using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asernatat_3.Models
{
    public class Order
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Prise")]
        public decimal Prise { set; get; }

        [Display(Name = "Time")]
        public string Time { set; get; }
    }
}
