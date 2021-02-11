using System;

namespace nbot.common
{
    public class Validation
    {
        public static void ThrowIfArgumentIsNull<T>(T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
        }
    }
}
