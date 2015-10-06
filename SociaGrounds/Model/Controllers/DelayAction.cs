using Microsoft.Xna.Framework;

namespace SociaGrounds.Model.Controllers
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

        public bool Update(GameTime gameTime)
        {
            if (!Started) return false;

            TimeRemaining -= gameTime.ElapsedGameTime.Milliseconds;

            if (TimeRemaining <= 0)
            {
                Reset();
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Started = false;
            TimeRemaining = _delayStart;
        }
    }
}
