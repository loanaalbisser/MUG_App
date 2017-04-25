using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Xamarin.UITest;

namespace MUG_App.Test
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        private IApp _app;
        private readonly Platform _platform;

        private const int NumberOfEventItems = 3;
        private const int NumberOfOrganizerItems = 2;

        public Tests(Platform platform)
        {
            this._platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        [Explicit]
        public void StartREPL()
        {
            _app.Repl();
        }

        [Test]
        public void ShowEvents()
        {
            OpenMenu("Events");
            WaitForEvents();
            CheckNumberOfListViewItems(NumberOfEventItems);
            PullToRefresh();
            CheckNumberOfListViewItems(NumberOfEventItems);
            TapFirstItem();
        }

        [Test]
        public void ShowOrganizer()
        {
            OpenMenu("Organizers");
            WaitForOrganizers();
            CheckNumberOfListViewItems(NumberOfOrganizerItems);
        }

        [Test]
        public void ShowGroup()
        {
            OpenMenu("Group");
            CheckGroupTitle();
        }

        private void PullToRefresh()
        {
            _app.DragCoordinates(500, 250, 500, 900);
        }

        private void OpenMenu(string menuItem)
        {
            _app.Tap(c => c.Marked("OK"));
            _app.Tap(c => c.Marked(menuItem));
        }

        private void CheckGroupTitle()
        {
            var result = _app.Query(c => c.Class("FormsTextView")).FirstOrDefault();
            result?.Text.Should().Be("Mobile User Group Zentralschweiz");
        }

        private void WaitForEvents()
        {
            _app.WaitForElement(c => c.Marked("Mobile App Testing"));
        }

        private void WaitForOrganizers()
        {
            _app.WaitForElement(c => c.Marked("Luzern"));
        }

        private void CheckNumberOfListViewItems(int numberOfItems)
        {
            NumberOfListViewItems().Should().Be(numberOfItems);
        }

        private void TapFirstItem()
        {
            _app.Tap(c => c.Class("ListView").Index(0));
        }

        private int NumberOfListViewItems()
        {
            return _app.Query(c => c.Class("ViewCellRenderer_ViewCellContainer")).Count();
        }

    }
}

