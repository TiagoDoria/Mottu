using AuthAPI.Models;

namespace AuthAPI.Service.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Deliveryman user);
    }
}
