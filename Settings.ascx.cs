/*
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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using GitHubRepoStats.Components.Enumerations;

namespace GitHubRepoStats
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from GitHubRepoStatsSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : ModuleSettingsBase
    {        
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack) return;

                var settings = Components.Settings.Load(TabModuleSettings);

                txtRepoOwner.Text = settings.RepoOwner;
                txtRepoName.Text = settings.RepoName;

                var selectedItem = rbRelease.Items.FindByValue(((int)settings.ReleaseType).ToString());
                if (selectedItem != null)
                {
                    rbRelease.ClearSelection();
                    selectedItem.Selected = true; 
                }
                    

                selectedItem = rbPreRelease.Items.FindByValue(((int)settings.PreReleaseType).ToString());
                if (selectedItem != null)
                {
                    rbPreRelease.ClearSelection();
                    selectedItem.Selected = true;
                }
                    

                ckEnableDownloadCount.Checked = settings.EnableDownloadCounts;

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                ReleaseTypes releaseType;

                var settings = new Components.Settings
                                   {
                                       RepoOwner = txtRepoOwner.Text,
                                       RepoName = txtRepoName.Text,
                                       ReleaseType =
                                           Enum.TryParse(rbRelease.SelectedValue, out releaseType)
                                               ? releaseType
                                               : ReleaseTypes.Latest,
                                       PreReleaseType =
                                           Enum.TryParse(rbPreRelease.SelectedValue, out releaseType)
                                               ? releaseType
                                               : ReleaseTypes.Latest,
                                       EnableDownloadCounts = ckEnableDownloadCount.Checked,
                                   };

                settings.Save(TabModuleId);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}