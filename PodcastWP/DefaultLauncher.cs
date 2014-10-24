namespace PodcastWP
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Default implementation of <see cref="ILauncher"/>.
    /// </summary>
    internal class DefaultLauncher : ILauncher
    {
        /// <inheritdoc />
        public async Task LaunchUriAsync(Uri uri)
        {
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}