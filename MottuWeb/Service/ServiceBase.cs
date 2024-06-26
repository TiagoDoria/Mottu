﻿using MottuWeb.Models;
using MottuWeb.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static MottuWeb.Utils.Configs;

namespace MottuWeb.Service
{
    public class ServiceBase : IServiceBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ITokenProvider _tokenProvider;
        public ServiceBase(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MottuAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.Url);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDTO.ApiType)
                {
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete; break;
                    case ApiType.POST:
                        message.Method = HttpMethod.Post; break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put; break;
                    default:
                        message.Method = HttpMethod.Get; break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception e)
            {
                var dto = new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
