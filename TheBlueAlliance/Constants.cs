﻿using Windows.ApplicationModel;

namespace TBA
{
    internal static class Constants
    {
        public static string GetAppVersion()
        {

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }
        public static string AppId = "the-blue-alliance:windows:v" + GetAppVersion();
        public static string BaseUrl = "http://www.thebluealliance.com/api/v2/";
    }
}
