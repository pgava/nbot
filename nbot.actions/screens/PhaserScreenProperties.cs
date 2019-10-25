namespace nbot.actions.screens
{
    public class PhaserScreenProperties : IScreenProperties
    {
        private readonly double width;
        private readonly double height;
        private readonly double border;

        public PhaserScreenProperties(double width, double height, double border)
        {
            this.width = width;
            this.height = height;
            this.border = border;
        }
        public double Width => width;

        public double Height => height;

        public Point CheckLimits(Point current, Point next, bool canBounce)
        {
            bool hasLimit;
            var x = HorizontalLimit(current.X, next.X, out hasLimit);
            if (hasLimit && !canBounce)
            {
                return new Point(x, current.Y);
            }

            var y = HorizontalLimit(current.Y, next.Y, out hasLimit);
            if (hasLimit && !canBounce)
            {
                return new Point(current.X, y);
            }

            return new Point(x, y);
        }

        private double HorizontalLimit(double curX, double x, out bool hasLimit)
        {
            var newX = curX + x;
            if (newX > width)
            {
                hasLimit = true;
                return width - border;
            }
            if (newX < 0)
            {
                hasLimit = true;
                return 0 + border;
            }

            hasLimit = false;
            return newX;
        }
        private double VeriticalLimit(double curY, double y, out bool hasLimit)
        {
            var newY = curY + y;
            if (newY > height)
            {
                hasLimit = true;
                return height - border;
            }
            if (newY < 0)
            {
                hasLimit = true;
                return 0 + border;
            }

            hasLimit = false;
            return newY;
        }
    }
}