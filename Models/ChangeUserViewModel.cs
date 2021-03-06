﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asernatat_3.Models
{
    public class ChangeUserViewModel
    {
        public IdentityUser User { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public IEnumerable<string> AllRoles { get; set; }
    }
}
