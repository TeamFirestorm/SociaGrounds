using System;

namespace SociaGrounds.Model.GUI
{
    public class DelayedAction
    {
        private readonly float _delayStart;

        public DelayedAction(float delay)
        {
            _delayStart = TimeRemaining = delay;
            Delay = delay;
        }

        public float Delay { get; private set; }
        public float TimeRemaining { get; private set; }
        public bool Started { get; set; }

        public void Update(float deltaTime)
        {
            if (!Started) return;

            TimeRemaining -= deltaTime;

            if (TimeRemaining <= 0)
            {
                Started = false;
                TimeRemaining = _delayStart;
            }
        }
    }
}
