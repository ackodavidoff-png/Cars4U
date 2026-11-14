using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebApplication1.Data.Models.Enums;

namespace WebApplication1.Services.Data.Car.Response
{
    public class GetAllCarsResponseModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public EngineType EngineType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public int HorsePower { get; set; }
        public byte[]? Image { get; set; }
        public State State { get; set; }
        public string? Description { get; set; }
        public string SellerId { get; set; } = null!;
    }
}
