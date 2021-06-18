using Microsoft.VisualStudio.TestTools.UnitTesting;
using FairPlayDating.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairPlayDating.Server.Tests;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyPhotos;
using System.Net.Http.Json;
using FairPlayDating.Common.Global;

namespace FairPlayDating.Server.Controllers.Tests
{
    [TestClass()]
    public class FacebookControllerTests: TestsBase
    {
        [TestMethod()]
        public async Task GetMyPhotosTest()
        {
            var authorizedHttpClient = await base.CreateAuthorizedClientAsync(Role.User);
            var result = await authorizedHttpClient.GetFromJsonAsync<GetMyPhotosResponse>(Constants.ApiRoutes.GetMyPhotos);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.data.Length > 0);
        }
    }
}