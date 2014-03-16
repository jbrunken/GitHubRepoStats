<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GitHubRepoStats.View" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude ID="KO" runat="server" PathNameAlias="SharedScripts" FilePath="knockout.js" />
<dnn:DnnJsInclude ID="GHRS" runat="server"  FilePath="~/DesktopModules/GitHubRepoStats/GitHubRepoStats.js" />
<!-- ko stopBinding: true -->
<div id="<%=ClientID %>" class="ghrs" data-config="<%= HttpUtility.HtmlAttributeEncode(ConfigJson) %>" data-bind="">
    <div class="repoLoading heading-ghrs rounded-ghrs" data-bind="visible: !loaded()"><i class="icon-ghrs-arrows-cw animate-spin-ghrs"></i> <span data-bind="text: localizedText.loading"></span></div>
    <div class="repoInfo" data-bind="visible: loaded">
        <div class="heading-ghrs rounded-top-ghrs">
            <ul class="repoCounts">
                <li><a data-bind="attr: { href: subscribersUrl, title: localizedText.watchers }"><i class="icon-ghrs-eye"></i><span data-bind="text: subscriberCount"></span></a></li>
                <li><a data-bind="attr: { href: stargazersUrl, title: localizedText.stargazers }"><i class="icon-ghrs-star"></i><span data-bind="text: stargazerCount"></span></a></li>
                <li><a data-bind="attr: { href: forkUrl, title: localizedText.forks }"><i class="icon-ghrs-fork"></i><span data-bind="text: forkCount"></span></a></li>
            </ul>
            <i class="icon-ghrs-github"></i><a data-bind="attr: { href: url }"><span data-bind="text: name"></span></a>
        </div>
        <div class="body-ghrs">
            <ul class="repoLinks">
                <li><a data-bind="visible: hasWiki, attr: {href:wikiUrl, title:localizedText.viewWiki }"><span><i class="icon-ghrs-book-open"></i></span></a></li>
                <li><a data-bind="attr: { href: issuesUrl, title: localizedText.viewIssues }"><span><i class="icon-ghrs-info-circled"></i></span></a></li>
            </ul>
            <div data-bind="text: desc"></div>
            <div><span data-bind="text: localizedText.lastUpdated"></span> <span data-bind="text: lastUpdatedWithFormat"></span></div>
            <div data-bind="visible: hasReleases">
                <div data-bind="foreach: releases">
                    <div class="release">
                        <a data-bind="attr: { href: url }"><span class="releaseTitle" data-bind="text: name"></span></a><br/>
                        <span data-bind="text: $root.localizedText.released"></span> <span data-bind="text: publishDateWithFormat"></span>
                        <!-- ko foreach: assets -->
                            <a class="repoAsset" data-bind="attr: { href: url, title: label }"><i class="icon-ghrs-download-cloud"></i> <span data-bind="text: name"></span></a> <span class="assetDownloads" data-bind="visible: $root.enableDownloadCounts(), attr: { title: downloadCountText }" ><i class="icon-ghrs-chart-bar" ></i></span><br />
                        <!-- /ko -->
                    </div>
                </div>
            </div> 
        </div>
            
    </div>
</div>
<!-- /ko -->
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        var $element = $("#<%=ClientID%>");
        var config = $.parseJSON($element.attr('data-config'));
        var ghrs = new GetHubRepoStats($, ko, config);
        ghrs.init('#<%= ClientID %>');
    });
</script>