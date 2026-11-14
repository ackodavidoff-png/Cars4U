namespace WebApplication1.ViewModels.CarViewModels
{
    public class CarTableViewModel
    {
        public Guid Id { get; set; }
        public byte[]? Image { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public double Price { get; set; }
    }
}
