namespace nbot.contracts.screens
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

        public double HorizontalDirection(double curX, double x, double direction)
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
        public double VeriticalDirection(double curY, double y, double direction)
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