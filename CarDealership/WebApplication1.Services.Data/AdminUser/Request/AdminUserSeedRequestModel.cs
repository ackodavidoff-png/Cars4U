using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Services.Data.AdminUser.Request
{
    public class AdminUserSeedRequestModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TownId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
