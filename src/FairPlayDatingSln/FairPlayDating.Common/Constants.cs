using System;

namespace FairPlayDating.Common
{
    public static class Constants
    {
        public static class ClientRoutes
        {
            public const string MyPhotos = "/Users/MyPhotos";
            public const string MyAlbums = "/Users/MyAlbums";
        }

        public static class ApiRoutes
        {
            public const string GetMyPhotos = "api/Facebook/GetMyPhotos";
            public const string GetMyAlbums = "api/Facebook/GetMyAlbums";
        }
    }
}
