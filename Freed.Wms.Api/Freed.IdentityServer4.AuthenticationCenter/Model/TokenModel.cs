﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.IdentityServer4.AuthenticationCenter.Model
{
    public class TokenModel
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string token_type { get; set; }

        public string scope { get; set; }


    }
}
