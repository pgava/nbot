using Xunit;
using FluentAssertions;
using nbot.actions;
using nbot.actions.screens;

namespace nbot.engine.test
{
    public class PositionProviderTests
    {
        [Fact]
        public void Can_Calculate_Next_Position()
        {
            var screenProvider = new PhaserScreen(800, 600, 20);
            var positionProvider = new PositionProvider(screenProvider);

            var currentPosition = new Point(0, 0);
            var nextPosition = positionProvider.CalculatePosition(currentPosition, 10, 90);

            nextPosition.X.Should().Be(0);
            nextPosition.Y.Should().Be(10);
        }

        [Fact]
        public void Can_Calculate_Next_Position_Based_On_Current()
        {
            var screenProvider = new PhaserScreen(800, 600, 20);
            var positionProvider = new PositionProvider(screenProvider);

            var currentPosition = new Point(100, 100);
            var nextPosition = positionProvider.CalculatePosition(currentPosition, 10, 90);

            nextPosition.X.Should().Be(100);
            nextPosition.Y.Should().Be(110);
        }
    }

}
