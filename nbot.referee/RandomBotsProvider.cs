using System;
using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public class RandomBotsProvider : IRandomBotsProvider
    {
        public IEnumerable<INBot> RandomizeList(IList<INBot> items)
        {
            var rndItems = new List<INBot>();
            Random rnd = new Random();

            while (items.Count > 0)
            {
                int index = rnd.Next(1, items.Count);
                rndItems.Add(items[index - 1]);
                items.RemoveAt(index - 1);
            }

            return rndItems;
        }
    }
}