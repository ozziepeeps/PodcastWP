﻿namespace PodcastWP
{
    using System;
    using System.Collections.Generic;
    using PodcastWP.Extensions;

    public static class PodcastHelper
    {
        private const string PodcastScheme = "wp-podcast://";

        private const string PlayModeArgument = "playMode";
        private const string UiModeArgument = "uiMode";
        private const string CallbackUriArgument = "callbackUri";
        private const string CallbackNameArgument = "callbackName";
        private const string FeedUrlArgument = "feedUrl";
        private const string Subscribe = "Subscribe";

        /// <summary>
        /// Initializes static members of the <see cref="PodcastHelper"/> class.
        /// </summary>
        static PodcastHelper()
        {
            PodcastHelper.Launcher = new DefaultLauncher();
        }

        /// <summary>
        /// Gets or sets the <see cref="ILauncher"/> implementation.
        /// </summary>
        /// <remarks>Internal for unit testing purposes.</remarks>
        internal static ILauncher Launcher { get; set; }

        /// <summary>
        /// Launches a podcast app w/ a specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="playMode">The mode of playback.</param>
        /// <param name="uiMode">The mode of the UI.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after the podcast app finishes its command.</param>
        /// <param name="callbackName">The name of your app which could be displayed in the target podcast app</param>
        public static async void CommandPodcastApp(PodcastCommand command, PlayMode playMode = PlayMode.None, UiMode uiMode = UiMode.Standard, string callbackUri = "", string callbackName = "")
        {
            var uri = string.Format("{0}{1}/", PodcastScheme, command);

            var queryParams = new List<string>();

            if (playMode != PlayMode.None)
            {
                queryParams.Add(string.Format("{1}={0}", playMode, PlayModeArgument));
            }

            if (uiMode != UiMode.Standard)
            {
                queryParams.Add(string.Format("{1}={0}", uiMode, UiModeArgument));
            }

            if (!string.IsNullOrEmpty(callbackUri))
            {
                queryParams.Add(string.Format("{1}={0}", callbackUri, CallbackUriArgument));
            }

            if (!string.IsNullOrEmpty(callbackName))
            {
                queryParams.Add(string.Format("{1}={0}", callbackName, CallbackNameArgument));
            }

            var queryString = string.Join("&", queryParams);

            uri += !string.IsNullOrEmpty(queryString) ? string.Format("?{0}", queryString) : string.Empty;

            await PodcastHelper.Launcher.LaunchUriAsync(new Uri(uri));
        }

        /// <summary>
        /// Launches a podcast app w/ a request to subscribe to a specified podcast.
        /// </summary>
        /// <param name="feedUrl">Url for the podcast feed.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after the podcast app finishes its command.</param>
        /// <param name="callbackName">The name of your app which could be displayed in the target podcast app.</param>
        public static async void SubscribeToPodcast(Uri feedUrl, string callbackUri = "", string callbackName = "")
        {
            var uri = string.Format("{0}{1}/", PodcastScheme, Subscribe);

            var queryParams = new List<string>
                                  {
                                      string.Format(
                                          "{1}={0}",
                                          Uri.EscapeDataString(feedUrl.OriginalString),
                                          FeedUrlArgument)
                                  };

            if (!string.IsNullOrEmpty(callbackUri))
            {
                queryParams.Add(string.Format("{1}={0}", callbackUri, CallbackUriArgument));
            }

            if (!string.IsNullOrEmpty(callbackName))
            {
                queryParams.Add(string.Format("{1}={0}", callbackName, CallbackNameArgument));
            }

            var queryString = string.Join("&", queryParams);

            uri += !string.IsNullOrEmpty(queryString) ? string.Format("?{0}", queryString) : string.Empty;

            await PodcastHelper.Launcher.LaunchUriAsync(new Uri(uri));
        }

        /// <summary>
        /// Determines whether the specified URI uses the podcast URI scheme.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True if pocket data is present.</returns>
        public static bool HasPodcastUri(Uri uri)
        {
            if (uri.ToString().Contains(PodcastScheme))
            {
                return true;
            }

            var escapedProtocol = Uri.EscapeDataString(PodcastScheme);
            return uri.ToString().Contains(escapedProtocol);
        }

        /// <summary>
        /// Retrieves the podcast action.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The deserialised podcast action.</returns>
        public static PodcastAction RetrievePodcastAction(Uri uri)
        {
            string podcastUri;

            if (uri.ToString().Contains(PodcastScheme))
            {
                podcastUri = uri.ToString();
            }
            else
            {
                podcastUri = uri.ToString().Replace("/Protocol?encodedLaunchUri=", string.Empty);
                podcastUri = Uri.UnescapeDataString(podcastUri);
            }

            var commandString = new Uri(podcastUri, UriKind.Absolute).CommandString();
            var queryString = new Uri(podcastUri, UriKind.Absolute).QueryString();

            var command = (PodcastCommand)Enum.Parse(typeof(PodcastCommand), commandString, true);

            var playMode = queryString.ContainsKey(PlayModeArgument) ? (PlayMode)Enum.Parse(typeof(PlayMode), queryString[PlayModeArgument], true) : PlayMode.None;
            var uiMode = queryString.ContainsKey(UiModeArgument) ? (UiMode)Enum.Parse(typeof(UiMode), queryString[UiModeArgument], true) : UiMode.Standard;
            var callbackUri = queryString.ContainsKey(CallbackUriArgument) ? queryString[CallbackUriArgument] : string.Empty;
            var callbackName = queryString.ContainsKey(CallbackNameArgument) ? queryString[CallbackNameArgument] : string.Empty;
            var feedUrl = queryString.ContainsKey(FeedUrlArgument) ? new Uri(Uri.UnescapeDataString(queryString[FeedUrlArgument]), UriKind.RelativeOrAbsolute) : null;

            var action = new PodcastAction
            {
                Command = command,
                PlayMode = playMode,
                UiMode = uiMode,
                FeedUrl = feedUrl,
                CallbackUri = callbackUri,
                CallbackName = callbackName
            };

            return action;
        }
    }
}