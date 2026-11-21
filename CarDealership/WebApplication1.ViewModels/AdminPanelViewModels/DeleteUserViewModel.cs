using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ViewModels.AdminPanelViewModels
{
    public class DeleteUserViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
