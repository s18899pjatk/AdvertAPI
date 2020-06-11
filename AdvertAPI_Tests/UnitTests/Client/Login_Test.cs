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
    public class Login_Test
    {
        [Test]
        public void LoginWithCorrectData_Correct()
        {
            //Arrange

            var dbLayer = new Mock<IClientServiceDb>();
            var refreshToken = Guid.NewGuid();
            var loginR = new LoginRequest
            {
                Login = "qfq13",
                Password = "3123"
            };
            dbLayer.Setup(d => d.Login(loginR)).Returns(new LoginResponse
            {
                RefreshToken = refreshToken.ToString()
            });
            var cont = new ClientController(dbLayer.Object);


            //Act
            var res = cont.Login(loginR);
            //Assert
            Assert.NotNull(res);
            var r = (OkObjectResult)res;
            Assert.True(r.StatusCode == 200);
        }
    }
}
