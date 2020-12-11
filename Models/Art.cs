using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asernatat_3.Models
{
    public class Art
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Img")]
        public string Img { set; get; }

        [Display(Name = "Category")]
        public Category Category { set; get; }
    }
}

