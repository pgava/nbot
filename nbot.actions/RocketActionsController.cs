using System;
using System.Collections.Generic;
using nbot.actions.screens;

namespace nbot.actions
{
    public class RocketActionsController : IRocketActionsController
    {
        private List<IRocket> rocketList = new List<IRocket>();
        private IPositionProvider positionProvider;
        private IMovementManager speedometer;

        public IEnumerable<IRocket> rockets => rocketList.AsReadOnly();

        public RocketActionsController(IPositionProvider positionProvider, IMovementManager speedometer)
        {
            if (positionProvider is null)
            {
                throw new ArgumentNullException(nameof(positionProvider));
            }

            if (speedometer is null)
            {
                throw new ArgumentNullException(nameof(speedometer));
            }

            this.positionProvider = positionProvider;
            this.speedometer = speedometer;
        }

        public void Fire()
        {
            // TODO: check if OK based on some predefined rules e.g. how many rocket has already fired?

            rocketList.Add(new Rocket(positionProvider, speedometer));
        }

        public void CalculateTrajectories(Vector startAt)
        {
            rocketList.ForEach(r => r.CalculateTrajectory(startAt));
        }
    }
}