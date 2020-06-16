using AdvertAPI;
using AdvertAPI.Controllers;
using AdvertAPI.Entities;
using AdvertAPI.Models.Requests;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertAPI_Tests.IntegrationTests.Client
{
    [TestFixture]
    class RegisterTest
    {
        [Test]
        public void RegisterNotExistingUser_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.Register(new RegisterRequest
            {
                FirstName = "Mike",
                LastName = "Paul",
                Email = "make@gmail.com",
                Login = "mikke",
                Password = "mk229",
                Phone = "+48041141"
            });

            //Assert 
            Assert.IsNotNull(res);
            var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }

        [Test]
        public void RegisterExistingUser_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.Register(new RegisterRequest
            {
                FirstName = "Mike",
                LastName = "Paul",
                Email = "makey@gmail.com",
                Login = "Jan125",
                Password = "mk229",
                Phone = "+48041241"
            });

            //Assert 
            Assert.IsNotNull(res);
            var r = (BadRequestObjectResult)res;
            Assert.True(r.StatusCode == 400);
        }
    }
}
