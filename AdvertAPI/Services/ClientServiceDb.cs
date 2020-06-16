using AdvertAPI.Entities;
using AdvertAPI.Exceptions;
using AdvertAPI.Models.Requests;
using AdvertAPI.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertAPI.Services
{
    public class ClientServiceDb : IClientServiceDb
    {
        private CampaignDbContext _campaignDbContext;
        private readonly IConfiguration _configuration;


        public ClientServiceDb(IConfiguration configuration, CampaignDbContext campaignDbContext)
        {
            _campaignDbContext = campaignDbContext;
            _configuration = configuration;
        }

        public ClientServiceDb(CampaignDbContext campaignDbContext)
        {
            _campaignDbContext = campaignDbContext;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var exists = _campaignDbContext.Clients.Any(c => c.Login.Equals(request.Login));

            if (!exists)
            {
                throw new LoginDoesNotExistsException($"{request.Login} does not exists");
            }

            var client = _campaignDbContext.Clients.SingleOrDefault(b => b.Login.Equals(request.Login));


            bool verify = PasswordHashing.Validate(request.Password, client.Salt, client.Password);
            if (!verify)
            {
                throw new PasswordIsNotCorrectException($"password {request.Password} is not correct");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.Login),
                new Claim(ClaimTypes.Name, client.LastName),
                new Claim(ClaimTypes.Role,  "Client")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("faafsasfassdgdfger524312"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Artem",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid();
            client.RefreshToken = refreshToken.ToString();
            _campaignDbContext.SaveChanges();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.ToString()
            };
        }

        public RefreshTokenResponse RefreshToken(string rToken)
        {
            var exists = _campaignDbContext.Clients.Any(c => c.RefreshToken.Equals(rToken));

            if (!exists)
            {
                throw new TokenDoesNotExistsException($"{rToken} does not exists");
            }

            var client = _campaignDbContext.Clients.SingleOrDefault(b => b.RefreshToken.Equals(rToken));

            var claims = new[]
             {
                    new Claim(ClaimTypes.NameIdentifier, client.Login),
                    new Claim(ClaimTypes.Name, client.LastName),
                    new Claim(ClaimTypes.Role,  "Employee"),
                    new Claim(ClaimTypes.Role,  "Student"),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: "Artem",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid();

            client.RefreshToken = refreshToken.ToString();
            _campaignDbContext.SaveChanges();


            return new RefreshTokenResponse
            {
                IdClient = client.IdClient,
                AccessToken = accessToken,
                RefreshToken = refreshToken.ToString(),
                OldRefrshToken = rToken
            };
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var LogExists = _campaignDbContext.Clients.Any(c => c.Login.Equals(request.Login));
            var MailExists = _campaignDbContext.Clients.Any(c => c.Email.Equals(request.Email));
            var PhoneNumExists = _campaignDbContext.Clients.Any(c => c.Phone.Equals(request.Phone));

            if (LogExists || MailExists || PhoneNumExists)
            {
               throw new ClientExistsException("Such client is already exists");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.Login),
                new Claim(ClaimTypes.Name, request.LastName),
                new Claim(ClaimTypes.Role,  "Client"),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Artem",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid();
            var refreshT = refreshToken.ToString();
            var salt = PasswordHashing.GenerateSalt();
            var passwrd = PasswordHashing.Create(request.Password, salt);

            var client = new Client()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Login = request.Login,
                Password = passwrd,
                RefreshToken = refreshT,
                Salt = salt
            };
            _campaignDbContext.Add(client);
            _campaignDbContext.SaveChanges();

            return new RegisterResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshT
            };
        }

        public CampaignListResponse ListOfCampaigns(string login)
        {
            var exists = _campaignDbContext.Clients.Any(c => c.Login.Equals(login));
            if (!exists)
            {
                throw new LoginDoesNotExistsException($"{login} does not exists");
            }

            var client = _campaignDbContext.Clients.SingleOrDefault(b => b.Login.Equals(login));

            var campaigns = _campaignDbContext.Campaigns
                .Where(c => c.IdClient
                .Equals(client.IdClient))
                .OrderByDescending(c => c.StartDate)
                .Select(c => c.IdCampaign).ToList();

            var adds = new List<List<string>>();
            foreach (var cm in campaigns)
            {
                var banner = _campaignDbContext.Banners
                    .Where(b => b.IdCampaign == cm)
                    .Select(b => b.Name).ToList();
                adds.Add(banner);
            }
            return new CampaignListResponse()
            {
                IdClient = client.IdClient,
                LastName = client.LastName,
                Campaigns = campaigns,
                Banners = adds
            };
        }

        public CreateCampaignResponse CreateCampaign(CreateCampaignRequest request)
        {
            // def date Format 2020-01-01T00:00:00
            var exists = _campaignDbContext.Clients.Any(c => c.IdClient == request.IdClient);
            if (!exists)
            {
                throw new ClientDoesNotExsitsException($"client {request.IdClient} does not exists");
            }
            if (request.EndDate < request.StartDate)
            {
                throw new WrongDateException("campaign should last at least 1 second");
            }
            var heights = new List<decimal>();
            var numOfBuild = 0;
            var firstB = _campaignDbContext.Buildings
                .Where(b => b.IdBuilding == request.FromIdBuilding)
                .FirstOrDefault();
            heights.Add(firstB.Height);

            for (int i = request.FromIdBuilding + 1; i <= request.ToIdBuilding; i++)
            {
                var nextB = _campaignDbContext.Buildings
                .Where(b => b.IdBuilding == i)
                .FirstOrDefault();

                if (firstB == null || nextB == null)
                {
                    return null;
                }

                if (firstB.Street != nextB.Street || firstB.City != nextB.City)
                {
                    return null;
                }
                heights.Add(nextB.Height);
                numOfBuild++;
            }
            // looking for the height of our banners 
            var sorted = heights.OrderByDescending(v => v).ToArray();
            var firstBannerHeight = sorted[0];
            var secondBannerHeight = sorted[1] * numOfBuild;
            var price = (firstBannerHeight + secondBannerHeight) * request.PricePerSquareMeter;

            var campaign = new Campaign()
            {
                IdClient = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding
            };
            _campaignDbContext.Add(campaign);
            _campaignDbContext.SaveChanges();

            var banners = new List<int>();
            var banner1 = new Banner()
            {
                IdCampaign = campaign.IdCampaign,
                Area = firstBannerHeight,
                Name = "for campaign: " + campaign.IdCampaign,
                Price = price / 2
            };
            _campaignDbContext.Add(banner1);
            _campaignDbContext.SaveChanges();
            banners.Add(banner1.IdAdvertisement);

            var banner2 = new Banner()
            {
                IdCampaign = campaign.IdCampaign,
                Area = secondBannerHeight,
                Name = "for campaign: " + campaign.IdCampaign,
                Price = price / 2
            };
            _campaignDbContext.Add(banner2);
            _campaignDbContext.SaveChanges();
            banners.Add(banner2.IdAdvertisement);
            return new CreateCampaignResponse
            {
                IdCampaign = campaign.IdCampaign,
                TotalPrice = price,
                Banners = banners
            };
        }
    }
}
