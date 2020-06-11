using AdvertAPI;
using AdvertAPI.Controllers;
using AdvertAPI.Entities;
using AdvertAPI.Models.Requests;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
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
            var repository = new ClientServiceDb(AppData.Configuration, new CampaignDbContext());
            var controller = new ClientController(repository);

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
