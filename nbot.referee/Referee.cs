using System;
using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public class Referee : IReferee
    {
        public IBotControllerCollection Bots { get; }

        public Referee(IBotControllerCollection bots)
        {
            if (bots is null)
            {
                throw new ArgumentNullException(nameof(bots));
            }

            Bots = bots;
        }
        
        public void PlayMatch()
        {
            while (IsMatchActive())
            {
                StartBots();

                WaitEndTurn();

                ProcessTurn();
            }
        }

        private void ProcessTurn()
        {
            throw new NotImplementedException();
        }

        private void WaitEndTurn()
        {
            throw new NotImplementedException();
        }

        private void StartBots()
        {
            throw new NotImplementedException();
        }

        private bool IsMatchActive()
        {
            throw new NotImplementedException();
        }
    }
}
