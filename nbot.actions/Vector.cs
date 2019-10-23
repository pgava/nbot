using System;

namespace nbot.actions
{
    public struct Vector
    {
        public double X { get; }
        public double Y { get; }

        public Point Point() => new Point(X, Y);
        public double Direction { get; }

        public Vector(double x, double y, double direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public Vector(Point position, double direction)
        {
            X = position.X;
            Y = position.Y;
            Direction = direction;
        }
        
        internal static Point Rnd(double width, double height)
        {
            throw new NotImplementedException();
        }
    }
}