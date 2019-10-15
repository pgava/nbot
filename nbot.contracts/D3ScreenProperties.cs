namespace nbot.contracts
{
    public class D3ScreenProperties : IScreenProperties
    {
        private readonly double width;
        private readonly double height;
        private readonly double border;

        public D3ScreenProperties(double width, double height, double border)
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

            if (direction >= 0 && direction <= 90)
            {
                return newX;
            }
            else if (direction > 90 && direction <= 180)
            {
                return newX;
            }
            else if (direction > 180 && direction <= 270)
            {
                return newX;
            }
            else if (direction > 270 && direction <= 360)
            {
                return newX;
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

            if (direction >= 0 && direction <= 90)
            {
                return newY;
            }
            else if (direction > 90 && direction <= 180)
            {
                return newY;
            }
            else if (direction > 180 && direction <= 270)
            {
                return newY;
            }
            else if (direction > 270 && direction <= 360)
            {
                return newY;
            }

            return newY;
        }
    }
}