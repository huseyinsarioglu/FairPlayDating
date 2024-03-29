﻿using System;

namespace FairPlayDating.Common.Global
{
    public static class Constants
    {
        public class Hubs
        {
            public const string NotificationHub = "/NotificationHub";
            public const string ReceiveMessage = "ReceiveMessage";
            public const string SendMessage = "SendMessage";
        }

        public class ConfigurationKeysNames
        {
            public const string AzureAppConfigConnectionString = "AzureAppConfigConnectionString";
            public const string DefaultConnectionString = "Default";
        }
        public class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
        public class Claims
        {
            public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
            public const string Name = "name";
            public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
            public const string SurName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
            public const string Emails = "emails";
        }
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
