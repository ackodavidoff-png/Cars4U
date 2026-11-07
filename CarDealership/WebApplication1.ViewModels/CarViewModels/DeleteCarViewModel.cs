using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ViewModels.CarViewModels
{
    public class DeleteCarViewModel
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public double Price { get; set; }
    }
}
