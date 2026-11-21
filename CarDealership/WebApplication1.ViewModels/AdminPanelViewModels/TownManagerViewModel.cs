using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ViewModels.AdminPanelViewModels
{
    public class TownManagerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UsersCount { get; set; }
        public int CarsCount { get; set; }
    }
}