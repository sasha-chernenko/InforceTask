using Xunit;
using Moq;
using Moq.EntityFrameworkCore;
using InforceTask.Context;
using InforceTask.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InforceTask.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InforceTask.Test
{
    public class InforceTest
    {
        private readonly Mock<URLContext> _context;
        private readonly URLController _controller;
        public InforceTest()
        {
            _context = new Mock<URLContext>();
            _controller = new URLController(_context.Object);
        }

        [Fact]
        public void UrlTableActionReturnsView()
        {
            var result = _controller.Table();
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task UrlGetActionReturnsOk()
        {
            var data = new List<URL>
            {
                new URL { Long = "https://www.youtube.com/" },
                new URL { Long = "https://www.google.com/" }
            };
            var mockSet = new Mock<URLContext>();
            mockSet.Setup(x => x.Urls).ReturnsDbSet(data);
            var contr = new URLController(mockSet.Object);
            
            var result = await contr.Get();
            var okResult = result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}   