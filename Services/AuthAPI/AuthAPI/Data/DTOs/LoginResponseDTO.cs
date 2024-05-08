namespace AuthAPI.Data.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO Deliveryman { get; set; }
        public string Token { get; set; }
    }
}
