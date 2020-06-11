using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdvertAPI.Entities;
using AdvertAPI.Models.Requests;
using AdvertAPI.Models.Responses;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private IClientServiceDb _clientServiceDb { get; set; }

        public ClientController(IClientServiceDb clientServiceDb)
        {
            _clientServiceDb = clientServiceDb;
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            var res = _clientServiceDb.Register(request);
            if (res == null)
            {
                return BadRequest("Such client is already exists");
            }
            return Ok(res);
        }

        //[Authorize(Roles = "Employee")]
        [HttpPost("refresh-token/{rToken}")]
        public IActionResult RefreshToken(string rToken)
        {
            var res = _clientServiceDb.RefreshToken(rToken);
            if (res == null)
            {
                return BadRequest("Such token does not exists in a database");
            }
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var res = _clientServiceDb.Login(request);
            if (res == null)
            {
                return StatusCode(401, "Login or password is not correct");
            }
            return Ok(res);
        }

        [Authorize]     // need access token to test
        [HttpGet("campaigns")]
        public IActionResult ListOfCampaigns()
        {
            // getting the login of the user from his claims 
            var login = User.Claims.Where(c => c.Issuer == "Artem").FirstOrDefault().Value;
            var res = _clientServiceDb.ListOfCampaigns(login);
            if (res == null)
            {
                return StatusCode(401);
            }
            return Ok(res);
        }
        [HttpPost("createCampaign")]
        public IActionResult CreateCampaign(CreateCampaignRequest request)
        {

            var res = _clientServiceDb.CreateCampaign(request);
            if (res == null)
            {
                return StatusCode(400);
            }
            return Ok(res);
        }   
    }
}

