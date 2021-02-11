using System;

namespace nbot.common
{
    public class NBotMath
    {
        public static double RadianToDegree(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

        public static double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

    }
}
