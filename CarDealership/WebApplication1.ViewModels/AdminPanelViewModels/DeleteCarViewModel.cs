using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ViewModels.AdminPanelViewModels
{
    public class DeleteCarViewModel
    {
        public Guid Id { get; set; }
        public byte[]? Image { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SellerId { get; set; } = null!;
        public string SellerFirstName { get; set; } = null!;
        public string SellerLastName { get; set; } = null!;
        public string SellerUsername { get; set; } = null!;
        public string TownName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
