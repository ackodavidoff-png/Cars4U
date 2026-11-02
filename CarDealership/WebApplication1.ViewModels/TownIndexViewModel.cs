using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;

namespace WebApplication1.ViewModels
{
    public class TownIndexViewModel
    {
        public IEnumerable<Town> Towns { get; set; }
    }
}
