using FluentAssertions;
using NUnit.Framework;

namespace MUG_App.Test.Unit.Event
{
    [TestFixture]
    public class EventFixture
    {
        private MUG_App.Event.Event _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new MUG_App.Event.Event();
        }

        [Test]
        public void Title_IsInitialized_ByConstructor()
        {
            // Act
            var result = _testee.Title;

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void Title_CanBeModified()
        {
            // Arrange
            const string newTitle = "New Title";

            // Act
            _testee.Title = newTitle;

            // Assert
            _testee.Title.Should().Be(newTitle);
        }

        [Test]
        public void Description_IsInitialized_ByConstructor()
        {
            // Act
            var result = _testee.Description;

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void Description_CanBeModified()
        {
            // Arrange
            const string newDescription = "New Description";

            // Act
            _testee.Description = newDescription;

            // Assert
            _testee.Description.Should().Be(newDescription);
        }

        [Test]
        public void ToString_ReturnsCorrectResult()
        {
            // Arrange
            _testee.Title = "Hello";
            _testee.Description = "World";

            // Act
            var result = _testee.ToString();

            // Assert
            result.Should().Be("Hello: World");
        }
    }
}
