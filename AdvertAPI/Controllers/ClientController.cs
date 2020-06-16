using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdvertAPI.Entities;
using AdvertAPI.Exceptions;
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
            RegisterResponse res;
            try
            {
                 res = _clientServiceDb.Register(request);
            }catch(ClientExistsException exc)
            {
                return BadRequest(exc.Message);
            }
            return Ok(res);
        }

        //[Authorize(Roles = "Employee")]
        [HttpPost("refresh-token/{rToken}")]
        public IActionResult RefreshToken(string rToken)
        {
            RefreshTokenResponse res;
            try
            {
                res = _clientServiceDb.RefreshToken(rToken);
            }catch(TokenDoesNotExistsException exc)
            {
                return BadRequest(exc.Message);
            }
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            LoginResponse res;
            try
            {
                res = _clientServiceDb.Login(request);
            } catch (Exception exc) when (exc is LoginDoesNotExistsException 
            || exc is PasswordIsNotCorrectException){
                return BadRequest(exc.Message);
            }
            return Ok(res);
        }

        [Authorize]     // need access token to test
        [HttpGet("campaigns")]
        public IActionResult ListOfCampaigns()
        {
            // getting the login of the user from his claims 
            var login = User.Claims.Where(c => c.Issuer == "Artem").FirstOrDefault().Value;

            CampaignListResponse res;
            try
            {
                res = _clientServiceDb.ListOfCampaigns(login);

            } catch(LoginDoesNotExistsException exc)
            {
                return BadRequest(exc.Message);
            }
            return Ok(res);
        }

        [HttpPost("createCampaign")]
        public IActionResult CreateCampaign(CreateCampaignRequest request)
        {
            CreateCampaignResponse res;
            try
            {
                res = _clientServiceDb.CreateCampaign(request);
            }catch(Exception e) when (e is ClientDoesNotExsitsException 
            || e is WrongDateException)
            {
                return BadRequest(e.Message);
            }
            return Ok(res);
        }   
    }
}

