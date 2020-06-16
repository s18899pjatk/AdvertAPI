using AdvertAPI;
using AdvertAPI.Controllers;
using AdvertAPI.Entities;
using AdvertAPI.Models.Requests;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AdvertAPI_Tests.IntegrationTests.Client
{
    [TestFixture]
    class LoginTest
    {
        [Test]
        public void LoginExistingUser_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration,campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.Login(new LoginRequest
            {
                Login = "Jan125",
                Password = "asd124"
            });

            //Assert 
            Assert.IsNotNull(res);
              var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }
    }
}
