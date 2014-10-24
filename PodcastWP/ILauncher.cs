namespace PodcastWP
{
    using System;
    using System.Threading.Tasks;

    internal interface ILauncher
    {
        Task LaunchUriAsync(Uri uri);
    }
}