using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.GCommon;

namespace WebApplication1.ViewModels.CarViewModels
{
    public class SearchCarsViewModel
    {
        public string? SearchText { get; set; }
        public PaginatedList<Car> Cars { get; set; }
    }
}
