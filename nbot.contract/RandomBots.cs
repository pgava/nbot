using System;
using System.Collections.Generic;

namespace nbot.contract
{
    public class RandomBots : IRandomBots
    {
        public IEnumerable<IBotController> RandomizeList(IList<IBotController> items)
        {
            var rndItems = new List<IBotController>();
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