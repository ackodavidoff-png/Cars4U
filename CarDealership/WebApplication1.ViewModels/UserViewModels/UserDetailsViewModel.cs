using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;

namespace WebApplication1.ViewModels.UserViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TownName { get; set; } = null!;
        public IEnumerable<Car> Cars { get; set; }
    }
}
