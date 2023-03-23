using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Forget
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}