using AdvertAPI.Controllers;
using AdvertAPI.Models.Requests;
using AdvertAPI.Models.Responses;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertAPI_Tests.UnitTests.Client
{
    [TestFixture]
    class CreateCampaign_Test
    {
        [Test]
        public void CreateCampaign_Correct()
        {

            //Arrange
            var dbLayer = new Mock<IClientServiceDb>();
            var req = new CreateCampaignRequest
            {
                IdClient = 2,
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 3, 1),
                FromIdBuilding = 4,
                ToIdBuilding = 6,
                PricePerSquareMeter = 35
            };
            dbLayer.Setup(d => d.CreateCampaign(req))
                .Returns(new CreateCampaignResponse
                {
                    IdCampaign = 2,
                    TotalPrice = 1130,
                    Banners = new List<int>
                    {
                        4,5,6
                    }
                });
            var cont = new ClientController(dbLayer.Object);

            //Act
            var res = cont.CreateCampaign(req);

            //Assert
            Assert.NotNull(res);
            var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }
    }
}
