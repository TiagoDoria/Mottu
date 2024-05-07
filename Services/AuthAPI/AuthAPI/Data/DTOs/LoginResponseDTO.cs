namespace AuthAPI.Data.DTOs
{
    public class LoginResponseDTO
    {
        public DeliverymanDTO Deliveryman { get; set; }
        public string Token { get; set; }
    }
}
