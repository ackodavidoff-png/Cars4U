using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ViewModels.AdminPanelViewModels
{
    public class CarManagerViewModel
    {
        public Guid Id { get; set; }
        public byte[]? Image { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string SellerId { get; set; } = null!;
        public string SellerFirstName { get; set; } = null!;
        public string SellerLastName { get; set; } = null!;
        public string SellerUsername { get; set; } = null!;
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
