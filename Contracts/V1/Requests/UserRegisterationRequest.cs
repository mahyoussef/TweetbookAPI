﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.V1.Requests
{
    public class UserRegisterationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
