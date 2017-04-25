using NUnit.Framework;
using Xamarin.UITest;

namespace MUG_App.Test
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        [Explicit]
        public void StartREPL()
        {
            app.Repl();
        }

        private void MenuItem(string menuItem)
        {
            app.Tap(c => c.Marked("OK"));
            app.Tap(c => c.Marked(menuItem));
        }
        
    }
}

