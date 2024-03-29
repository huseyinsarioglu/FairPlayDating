﻿using FairPlayDating.Common;
using FairPlayDating.Common.Global;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyAlbums;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyPhotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FairPlayDating.Client.ClientServices
{
    public class FacebookClientService
    {
        private HttpClientService HttpClientService { get; }
        public FacebookClientService(HttpClientService httpClientService)
        {
            this.HttpClientService = httpClientService;
        }


        public async Task<GetMyPhotosResponse> GetMyPhotos(string pageToken=null)
        {
            string requestUrl = $"{Constants.ApiRoutes.GetMyPhotos}?pageToken={pageToken}";
            var authorizedHttpClinet = this.HttpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClinet.GetFromJsonAsync<GetMyPhotosResponse>(requestUrl);
            return result;
        }

        public async Task<GetMyAlbumsResponse> GetMyAlbums(string pageToken = null)
        {
            string requestUrl = $"{Constants.ApiRoutes.GetMyAlbums}?pageToken={pageToken}";
            var authorizedHttpClinet = this.HttpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClinet.GetFromJsonAsync<GetMyAlbumsResponse>(requestUrl);
            return result;
        }
    }
}
