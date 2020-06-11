using AdvertAPI.Controllers;
using AdvertAPI.Models.Requests;
using AdvertAPI.Models.Responses;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.UnitTests.Client
{
    [TestFixture]
    public class RegisterClientTest
    {
        [Test]
        public void RegisterWithNotExistingData_Correct()
        {
            // Arrange
         
            var dbLayer = new Mock<IClientServiceDb>();
            var res = new RegisterRequest()
            {
                FirstName = "Mack",
                LastName = "Brown",
                Email = "dad@wp.pl",
                Phone = "454-33-222",
                Login = "Mocky221",
                Password = "asd124das"
            };


            dbLayer.Setup(d => d.Register(res)).Returns(new RegisterResponse()
            {
                RefreshToken = Guid.NewGuid().ToString()
            });
            var cont = new ClientController(dbLayer.Object);

            //Act 
            var result = cont.Register(res);

            //Asserts
            Assert.NotNull(result);
            var r = (OkObjectResult)result;
            Assert.True(r.StatusCode == 200);

        }
      

    }
}
