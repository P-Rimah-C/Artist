using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asernatat_3.Models
{
    public class Category
    {
        public Guid Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public List<Art> Arts { get; set; }
    }
}