using System.Collections.Generic;
using nbot.contract;

namespace nbot.engine
{
    public class MovesProcess : IMovesProcess
    {
        public IEnumerable<IMove> ProcessNextMove(IEnumerable<IBotController> bots)
        {
            var moves = new List<IMove>();

            foreach (var bot in bots)
            {
                moves.Add(new Move(bot.CalculateNextPosition(), ItemType.bot));
            }

            return moves;
        }
    }
}