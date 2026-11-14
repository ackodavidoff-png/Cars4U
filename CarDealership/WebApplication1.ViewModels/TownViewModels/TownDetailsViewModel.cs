using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.GCommon;
using WebApplication1.ViewModels.CarViewModels;

namespace WebApplication1.ViewModels.TownViewModels
{
    public class TownDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public PaginatedList<CarTableViewModel> Cars { get; set; }
    }
}
