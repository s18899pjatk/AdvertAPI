using AdvertAPI;
using AdvertAPI.Controllers;
using AdvertAPI.Entities;
using AdvertAPI.Models.Requests;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertAPI_Tests.IntegrationTests.Client
{
    [TestFixture]
    class CreateCampaignTest
    {
        [Test]
        public void CreateCampaign_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.CreateCampaign(new CreateCampaignRequest
            {
                IdClient = 2,
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020,3,1),
                FromIdBuilding = 4,
                ToIdBuilding = 6,
                PricePerSquareMeter = 40
            });

            //Assert 
            Assert.IsNotNull(res);
            var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }

        [Test]
        public void CreateCampaignBuilingsNotOnTheSameStreet_Correct()
        {
            //Arrange
            var campaignDbContext = new CampaignDbContext();
            var dbService = new ClientServiceDb(AppData.Configuration, campaignDbContext);
            var controller = new ClientController(dbService);

            //Act
            var res = controller.CreateCampaign(new CreateCampaignRequest
            {
                IdClient = 2,
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 3, 1),
                FromIdBuilding = 3,
                ToIdBuilding = 6,
                PricePerSquareMeter = 40
            });

            //Assert 
            Assert.IsNotNull(res);
            var r = (BadRequestObjectResult)res;
            Assert.True(r.StatusCode == 400);
        }
    }
}
