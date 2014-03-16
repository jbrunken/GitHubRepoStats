function GetHubRepoStats($, ko, options) {

    var viewModel = null;
    var $element = null;

    var apiUrl = "https://api.github.com/repos/" + options.repoOwner + "/" + options.repoName;

    function repositoryViewModel() {

        var self = this;

        this.repoLoaded = ko.observable(false);
        this.releasesLoaded = ko.observable(false);
        this.loaded = ko.computed(function () { return self.repoLoaded() && self.releasesLoaded(); });

        // Localization
        this.localizedText = options.txt || { };

        // Repository
        this.name = ko.observable();
        this.url = ko.observable();
        this.desc = ko.observable();
        this.subscriberCount = ko.observable();
        this.forkCount = ko.observable();
        this.stargazerCount = ko.observable();
        this.hasWiki = ko.observable();
        this.lastUpdated = ko.observable();
        this.subscribersUrl = ko.computed(function () { return self.url() + "/watchers"; });
        this.forkUrl = ko.computed(function () { return self.url() + "/network"; });
        this.stargazersUrl = ko.computed(function () { return self.url() + "/stargazers"; });
        this.wikiUrl = ko.computed(function () { return self.url() + "/wiki"; });
        this.issuesUrl = ko.computed(function () { return self.url() + "/issues"; });
        this.enableDownloadCounts = ko.observable(options.enableDownloadCounts);
        this.lastUpdatedWithFormat = ko.computed(function() {
            var d = self.lastUpdated();
            if (!d) return "";
            return d.toLocaleString();
        });
        

        // Releases
        this.releases = ko.observableArray([]);

        this.hasReleases = ko.computed(function () { return self.releases() && self.releases().length; });
    }

    function releaseViewModel() {

        var self = this;

        this.prerelease = ko.observable();
        this.name = ko.observable();
        this.url = ko.observable();
        this.publishDate = ko.observable();
        this.assets = ko.observableArray();
        this.publishDateWithFormat = ko.computed(function () {
            var d = self.publishDate();
            if (!d) return "";
            return d.toLocaleString();
        });
    }
    
    function assetViewModel() {

        var self = this;

        this.downloadCount = ko.observable();
        this.url = ko.observable();
        this.name = ko.observable();
        this.label = ko.observable();
        this.downloadCountText = ko.computed(function() {  return options.txt.downloads + " " + self.downloadCount(); });
    }

    this.init = function (elementId) {

        $element = $(elementId);

        // Create our ViewModel

        viewModel = new repositoryViewModel();

        ko.bindingHandlers.stopBinding = {
            init: function () {
                return { controlsDescendantBindings: true };
            }
        };

        ko.virtualElements.allowedBindings.stopBinding = true;

        ko.applyBindings(viewModel, $element[0]);

        // Get Repo Info
        $.getJSON(apiUrl)
            .done(function(data) { loadRepoData(data); })
            .always(function() { viewModel.repoLoaded(true); });


        viewModel.releasesLoaded(true);

        // Get Release Info if needed
        if (options.releaseType || options.preReleaseType) {
            $.getJSON(apiUrl + "/releases")
                .done(function (data) { loadReleasesData(data); })
                .always(function () { viewModel.releasesLoaded(true); });
        }
    };
    
    // Private methods
    
    function loadRepoData(data) {
        
        viewModel.name(data.full_name);
        viewModel.url(data.html_url);
        viewModel.desc(data.description);
        viewModel.subscriberCount(data.subscribers_count);
        viewModel.forkCount(data.forks_count);
        viewModel.stargazerCount(data.stargazers_count);
        viewModel.hasWiki(data.has_wiki);
        viewModel.lastUpdated(new Date(data.updated_at));
    }
    
    function loadReleasesData(data) {

        if (!data || !data.length);

        var releases = [];

        var lastReleaseAdded, lastPreReleaseAdded;

        $.each(data, function(releaseIndex, releaseData) {

            var publishDate = new Date(releaseData.published_at);

            // Don't add if any of these conditions are met
            if (releaseData.draft || (!options.releaseType && !releaseData.prerelease) || (!options.preReleaseType && releaseData.prerelease)) {
                return;
            }

            // Handle release
            if(!releaseData.prerelease && options.releaseType === 2 && lastReleaseAdded) {

                if (publishDate <= lastReleaseAdded.publishDate())
                    return; //Skip this item
                
                // Remove the previous release before we add the new one
                releases = $.grep(releases, function(element) { return element !== lastReleaseAdded; });
            }
            
            // Handle prerelease
            if (releaseData.prerelease && options.preReleaseType === 2 && lastPreReleaseAdded) {
                if (publishDate <= lastPreReleaseAdded.publishDate())
                    return; //Skip this item

                // Remove the previous prerelease before we add the new one
                releases = $.grep(releases, function(element) { return element !== lastPreReleaseAdded; });
            }

            // Add the item

            var assets = [];
            
            if (releaseData.assets && releaseData.assets.length) {
                $.each(releaseData.assets, function (assetIndex, assetData) {

                    console.log(assetData);

                    var asset = new assetViewModel();
                    asset.name(assetData.name);
                    asset.label(assetData.label);
                    asset.url("https://github.com/" + options.repoOwner + "/" + options.repoName + "/releases/download/" + releaseData.tag_name + "/" + assetData.name);
                    asset.downloadCount(assetData.download_count);
                    assets.push(asset);
                });
            }

            var release = new releaseViewModel();
            release.prerelease(releaseData.prerelease);
            release.assets(assets);
            release.publishDate(publishDate);
            release.name(releaseData.name);
            release.url(releaseData.html_url);
            releases.push(release);

            lastReleaseAdded = release;

        });

        viewModel.releases(releases);
    }
};
