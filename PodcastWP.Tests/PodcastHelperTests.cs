namespace PodcastWP.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class PodcastHelperTests
    {
        private readonly Mock<ILauncher> launcher;

        public PodcastHelperTests()
        {
            this.launcher = new Mock<ILauncher>(MockBehavior.Strict);

            PodcastHelper.Launcher = this.launcher.Object;
        }

        [TestMethod]
        public void Launch()
        {
            var expectedUri = new Uri("wp-podcast://Launch");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.CommandPodcastApp(PodcastCommand.Launch);

            this.launcher.VerifyAll();
        }

        [TestMethod]
        public void Play()
        {
            var expectedUri = new Uri("wp-podcast://Play");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.CommandPodcastApp(PodcastCommand.Play);

            this.launcher.VerifyAll();
        }

        [TestMethod]
        public void Pause()
        {
            var expectedUri = new Uri("wp-podcast://Pause");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.CommandPodcastApp(PodcastCommand.Pause);

            this.launcher.VerifyAll();
        }

        [TestMethod]
        public void SkipNext()
        {
            var expectedUri = new Uri("wp-podcast://SkipNext");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.CommandPodcastApp(PodcastCommand.SkipNext);

            this.launcher.VerifyAll();
        }

        [TestMethod]
        public void SkipPrevious()
        {
            var expectedUri = new Uri("wp-podcast://SkipPrevious");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.CommandPodcastApp(PodcastCommand.SkipPrevious);

            this.launcher.VerifyAll();
        }

        [TestMethod]
        public void Subscribe()
        {
            var expectedUri = new Uri("wp-podcast://Subscribe/?feedUrl=http%3A%2F%2Ftwit.tv%2Fww");

            this.launcher.Setup(s => s.LaunchUriAsync(expectedUri)).Verifiable();

            PodcastHelper.SubscribeToPodcast(new Uri("http://twit.tv/ww"));

            this.launcher.VerifyAll();
        }
    }
}