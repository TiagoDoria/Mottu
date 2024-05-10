using MottuWeb.Models;

namespace MottuWeb.Service.IService
{
    public interface IServiceBase
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);
    }
}
