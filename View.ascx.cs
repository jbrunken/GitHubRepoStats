﻿/*
' Copyright (c) 2014  Jason Brunken
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;



namespace GitHubRepoStats
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from GitHubRepoStatsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : PortalModuleBase
    {
        public string ConfigJson
        {
            get
            {
                var settings = Components.Settings.Load(Settings);

                var localizedText = new
                {
                    loading = LocalizeString("Loading.Text"),
                    watchers = LocalizeString("Watchers.Text"),
                    forks = LocalizeString("Forks.Text"),
                    stargazers = LocalizeString("Stargazers.Text"),
                    lastUpdated = LocalizeString("LastUpdated.Text"),
                    preRelease = LocalizeString("PreRelease.Text"),
                    released = LocalizeString("Released.Text"),
                    downloads = LocalizeString("Downloads.Text"),
                    viewWiki = LocalizeString("ViewWiki.Text"),
                    viewIssues = LocalizeString("ViewIssues.Text")
                };

                var config = new
                {
                    txt = localizedText,
                    releaseType = settings.ReleaseType,
                    preReleaseType = settings.PreReleaseType,
                    repoName = settings.RepoName,
                    repoOwner = settings.RepoOwner,
                    enableDownloadCounts = settings.EnableDownloadCounts
                };

                return new JavaScriptSerializer().Serialize(config);
            }
        }

        /*
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nothing to do if it's a callback
            if(Page.IsCallback)
                return;
            
            try
            {

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
         */
    }
}