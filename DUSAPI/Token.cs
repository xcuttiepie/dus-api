﻿using System;
using System.Collections.Generic;

namespace DUSAPI
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public string UserId { get; set; } = null!;
        public string AuthToken { get; set; } = null!;
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool? IsExpire { get; set; }
    }
}
