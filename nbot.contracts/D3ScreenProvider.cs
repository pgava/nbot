namespace nbot.contracts
{
    public class D3ScreenProvider : IScreenProvider
    {
        private readonly double width;
        private readonly double height;

        public D3ScreenProvider(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
        public double Width => width;

        public double Height => height;

        public double HorizontalDirection(double x, double direction)
        {
            if (direction > 0 && direction <= 90)
            {
                return x;
            }
            else if (direction > 90 && direction <= 180)
            {
                return x;
            }
            else if (direction > 180 && direction <= 270)
            {
                return -x;
            }
            else if (direction > 270 && direction <= 360)
            {
                return -x;
            }

            return x;
        }
        public double VeriticalDirection(double y, double direction)
        {
            if (direction > 0 && direction <= 90)
            {
                return -y;
            }
            else if (direction > 90 && direction <= 180)
            {
                return y;
            }
            else if (direction > 180 && direction <= 270)
            {
                return y;
            }
            else if (direction > 270 && direction <= 360)
            {
                return -y;
            }

            return y;
        }
    }
}