namespace MottuWeb.Models
{
    public class MotorcycleDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public bool Available { get; set; } = true;
    }
}
