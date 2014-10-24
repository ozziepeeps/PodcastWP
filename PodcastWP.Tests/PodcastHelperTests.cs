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
    }
}