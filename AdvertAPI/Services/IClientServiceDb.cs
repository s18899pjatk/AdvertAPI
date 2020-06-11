using AdvertAPI.Models.Requests;
using AdvertAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Services
{
    public interface IClientServiceDb
    {
        public RegisterResponse Register(RegisterRequest request);
        public RefreshTokenResponse RefreshToken(string rToken);
        public LoginResponse Login(LoginRequest request);
        public CampaignListResponse ListOfCampaigns(string login);
        public CreateCampaignResponse CreateCampaign(CreateCampaignRequest request);
    }
}
