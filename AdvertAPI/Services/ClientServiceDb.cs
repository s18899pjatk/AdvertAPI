using AdvertAPI.Entities;
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

        public LoginResponse Login(LoginRequest request)
        {
            var exists = _campaignDbContext.Clients.Any(c => c.Login.Equals(request.Login));

            if (!exists)
            {
                return null;
            }

            var client = _campaignDbContext.Clients.SingleOrDefault(b => b.Login.Equals(request.Login));


            bool verify = PasswordHashing.Validate(request.Password, client.Salt, client.Password);
            if (!verify)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.Login),
                new Claim(ClaimTypes.Name, client.LastName),
                new Claim(ClaimTypes.Role,  "Client")
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
                return null;
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
                return null;
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
                return null;
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
    }
}
