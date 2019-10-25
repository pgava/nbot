using System;
using System.Collections.Generic;
using nbot.actions.screens;

namespace nbot.actions
{
    public class RocketActionsController : IRocketActionsController
    {
        private List<IRocket> rocketList = new List<IRocket>();
        private IHelm helm;
        private ISpeedometer speedometer;

        public IEnumerable<IRocket> rockets => rocketList.AsReadOnly();

        public RocketActionsController(IHelm helm, ISpeedometer speedometer)
        {
            if (helm is null)
            {
                throw new ArgumentNullException(nameof(helm));
            }

            if (speedometer is null)
            {
                throw new ArgumentNullException(nameof(speedometer));
            }

            this.helm = helm;
            this.speedometer = speedometer;
        }

        public void Fire()
        {
            // TODO: check if OK based on some predefined rules e.g. how many rocket has already fired?

            rocketList.Add(new Rocket(helm, speedometer));
        }

        public void CalculateTrajectories(Vector startAt)
        {
            rocketList.ForEach(r => r.CalculateTrajectory(startAt));
        }
    }
}