using AdvertAPI;
using AdvertAPI.Controllers;
using AdvertAPI.Entities;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertAPI_Tests.IntegrationTests.Client
{
    [TestFixture]
    class RefreshTokenTest
    {
        [Test]
        public void RefreshTokenExists_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.RefreshToken("138c3168-2e76-463d-8665-b0e5bfb7b612");

            //Assert 
            Assert.IsNotNull(res);
            var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }

        [Test]
        public void RefreshTokenNotExists_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.RefreshToken("138c3168-2e76-463d-8665-adwqeq4211");

            //Assert 
            Assert.IsNotNull(res);
            var r = (BadRequestObjectResult)res;
            Assert.True(r.StatusCode == 400);
        }
    }
}
