using Xunit;
using FluentAssertions;
using nbot.actions;
using nbot.actions.screens;

namespace nbot.engine.test
{
    public class PhaserScreenTests
    {
        [Fact]
        public void Can_Calculate_Next_Position()
        {
            var screenProvider = new PhaserScreen(800, 600, 20);

            var nextPosition = screenProvider.MapNextPointToScreen(new Point(10, 10), new Point(20, 20));

            nextPosition.X.Should().Be(30);
            nextPosition.Y.Should().Be(30);
        }

        [Fact]
        public void Can_Calculate_Next_Position_When_Object_Goes_Too_High()
        {
            var screenProvider = new PhaserScreen(800, 600, 0);

            var nextPosition = screenProvider.MapNextPointToScreen(new Point(100, 550), new Point(0, 100));

            nextPosition.X.Should().Be(100);
            nextPosition.Y.Should().Be(600);
        }

        [Fact]
        public void Can_Calculate_Next_Position_When_Object_Goes_Too_Right()
        {
            var screenProvider = new PhaserScreen(800, 600, 0);

            var nextPosition = screenProvider.MapNextPointToScreen(new Point(750, 400), new Point(100, 100));

            nextPosition.X.Should().Be(800);
            nextPosition.Y.Should().Be(500);
        }

    }

}
