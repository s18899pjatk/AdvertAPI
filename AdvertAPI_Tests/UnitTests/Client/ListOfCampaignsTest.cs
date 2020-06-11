using AdvertAPI.Controllers;
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
    class ListOfCampaignsTest
    {
        [Test]
        public void ListOfCampaigns_Correct()
        {

            //Arrange

            var dbLayer = new Mock<IClientServiceDb>();
            dbLayer.Setup(d => d.ListOfCampaigns("dafasf21"))
                .Returns(new CampaignListResponse
                {
                IdClient = 1,
                LastName = "Broody",
                Banners = new List<List<string>>()
                {
                    new List<string>{ "Sprite" },
                    new List<string>{ "Coca-Cola" },
                    new List<string>{ "Rollin" }
                },
                Campaigns = new List<int>() { 1 }
                });
        }
    }
}
