namespace nbot.actions.screens
{
    public class PhaserScreen : IScreen
    {
        private readonly double width;
        private readonly double height;
        private readonly double border;

        public PhaserScreen(double width, double height, double border)
        {
            this.width = width;
            this.height = height;
            this.border = border;
        }
        public double Width => width;

        public double Height => height;

        public Point MapNextPointToScreen(Point current, Point next)
        {
            var x = HorizontalLimit(current.X, next.X);
            var y = VeriticalLimit(current.Y, next.Y);

            return new Point(x, y);
        }

        private double HorizontalLimit(double curX, double x)
        {
            var newX = curX + x;
            if (newX > width)
            {
                return width - border;
            }
            if (newX < 0)
            {
                return 0 + border;
            }

            return newX;
        }
        private double VeriticalLimit(double curY, double y)
        {
            var newY = curY + y;
            if (newY > height)
            {
                return height - border;
            }
            if (newY < 0)
            {
                return 0 + border;
            }

            return newY;
        }
    }
}