using System;
using System.Collections.Generic;

namespace QnA_app.Models
{
    public partial class SignupTbl
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Confirmpassword { get; set; } = null!;
    }
}
