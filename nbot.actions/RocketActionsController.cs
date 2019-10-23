using System;
using System.Collections.Generic;
using nbot.actions.screens;

namespace nbot.actions
{
    public class RocketActionsController : IRocketActionsController
    {       
        private readonly IScreenProperties screenProperties;   
        private List<IRocket> rocketList = new List<IRocket>();
        public IEnumerable<IRocket> rockets => rocketList.AsReadOnly();

        public RocketActionsController(IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;

        }       

        public void Fire()
        {
            // TODO: check if OK based on some predefined rules e.g. how many rocket has already fire?
            rocketList.Add(new Rocket(screenProperties));
        }

        public void CalculateNextPosition(Vector currentBotPosition)
        {
            throw new NotImplementedException();
        }
    }
}