﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.DTOs.Request
{
    public class UserSigninRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Google2FA { get; set; }
    }
}
