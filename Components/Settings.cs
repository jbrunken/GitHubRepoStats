using System;
using System.Collections;
using DotNetNuke.Entities.Modules;
using GitHubRepoStats.Components.Enumerations;

namespace GitHubRepoStats.Components
{
    public class Settings
    {
        public string RepoName { get; set; }
        public string RepoOwner { get; set; }
        public ReleaseTypes ReleaseType { get; set; }
        public ReleaseTypes PreReleaseType { get; set; }
        public bool EnableDownloadCounts;

        public bool Save(int tabModuleId)
        {
            var modules = new ModuleController();

            modules.UpdateTabModuleSetting(tabModuleId, "RepoOwner", RepoOwner == null ? string.Empty : RepoOwner.Trim());
            modules.UpdateTabModuleSetting(tabModuleId, "RepoName", RepoName == null ? string.Empty : RepoName.Trim());
            modules.UpdateTabModuleSetting(tabModuleId, "Releases", ((int)ReleaseType).ToString());
            modules.UpdateTabModuleSetting(tabModuleId, "PreReleases", ((int)PreReleaseType).ToString());
            modules.UpdateTabModuleSetting(tabModuleId, "EnableDownloadCounts", EnableDownloadCounts ? "1" : "0");

            return true;
        }

        public static Settings Load(Hashtable settings)
        {
            var result = new Settings();

            if (settings == null)
                return result;

            if (settings.Contains("RepoOwner"))
                result.RepoOwner = settings["RepoOwner"].ToString();

            if (settings.Contains("RepoName"))
                result.RepoName = settings["RepoName"].ToString();


            ReleaseTypes releaseType;

            result.ReleaseType = settings.Contains("Releases") && Enum.TryParse(settings["Releases"].ToString(), out releaseType)
                                ? releaseType
                                : ReleaseTypes.Latest; 

            result.PreReleaseType = settings.Contains("PreReleases") && Enum.TryParse(settings["PreReleases"].ToString(), out releaseType)
                                ? releaseType
                                : ReleaseTypes.Latest;


            bool boolVal;

            result.EnableDownloadCounts = !settings.Contains("EnableDownloadCount") || !bool.TryParse(settings["EnableDownloadCount"].ToString(), out boolVal) || boolVal;

            return result;
        }
    }
}