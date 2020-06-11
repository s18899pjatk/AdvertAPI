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
    class RefreshToken_Test
    {
        [Test]
        public void RefreshTheExistingToken()
        {
            //Arrange
            var dbLayer = new Mock<IClientServiceDb>();
            string token = "138c3168-2e76-463d-8665-b0e5bfb7b612";
            var refreshToken = Guid.NewGuid();
            dbLayer.Setup(d => d.RefreshToken(token)).Returns(new RefreshTokenResponse()
            {
                RefreshToken = refreshToken.ToString(),
                OldRefrshToken = token
            });
            var cont = new ClientController(dbLayer.Object);

            //Act 
            var result = cont.RefreshToken(token);
         
            //Asserts
            Assert.NotNull(result);
            var r = (OkObjectResult) result;
            Assert.True(r.StatusCode == 200);
        }
    }
}
