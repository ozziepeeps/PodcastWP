namespace PodcastWP
{
    /// <summary>
    /// Represents a command to a podcast app.
    /// </summary>
    public enum PodcastCommand
    {
        /// <summary>
        /// Launch the app.
        /// </summary>
        Launch,

        /// <summary>
        /// Play a podcast.
        /// </summary>
        Play,

        /// <summary>
        /// Pause a podcast.
        /// </summary>
        Pause,

        /// <summary>
        /// Skip to the next podcast.
        /// </summary>
        SkipNext,

        /// <summary>
        /// Skip to the previous podcast.
        /// </summary>
        SkipPrevious
    }
}