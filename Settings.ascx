<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GitHubRepoStats.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
<fieldset>
    <div class="dnnFormItem">
        <dnn:Label ID="lblRepoOwner" runat="server" /> 
        <asp:TextBox ID="txtRepoOwner" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblRepoName" runat="server" /> 
        <asp:TextBox ID="txtRepoName" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblReleases" runat="server" />
        <asp:RadioButtonList runat="server" ID="rbRelease" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" resourcekey="None" />
            <asp:ListItem Value="1" resourcekey="All" />
            <asp:ListItem Value="2" resourcekey="Latest" Selected="True" />
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblPreReleases" runat="server" />
        <asp:RadioButtonList runat="server" ID="rbPreRelease" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" resourcekey="None" />
            <asp:ListItem Value="1" resourcekey="All" />
            <asp:ListItem Value="2" resourcekey="Latest" Selected="True" />
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblOptions" runat="server" />
        <asp:CheckBox ID="ckEnableDownloadCount" runat="server" resourcekey="ckEnableDownloadCount" Checked="true" />
    </div>
</fieldset>
