using System;

namespace nbot.actions
{
    public struct Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        internal static Point Rnd(double width, double height)
        {
            throw new NotImplementedException();
        }        
    }
}