﻿using System;

namespace ExaminationsSystem.Application.Models.ViewModels.Identity
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
