using FairPlayDating.Client.ClientServices;
using FairPlayDating.Common;
using FairPlayDating.Common.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyPhotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FairPlayDating.Client.Pages.Users
{
    [Authorize]
    [Route(Constants.ClientRoutes.MyPhotos)]
    public partial class MyPhotos
    {
        [Inject]
        private FacebookClientService FacebookClientService { get; set; }
        private string PageToken { get; set; } = null;
        private GetMyPhotosResponse PagePhotos { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            this.PagePhotos = await this.FacebookClientService.GetMyPhotos(PageToken);
        }

        private async Task OnNextPageClick()
        {
            this.PageToken = this.PagePhotos.paging.cursors.after;
            await LoadData();
        }
    }
}
