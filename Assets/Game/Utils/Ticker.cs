using System.Collections.Generic;
using Zenject;

namespace Game.Utils
{
    public class Ticker : ITickable
    {
        private readonly List<ITickListener> _listeners = new();
        private bool _isPaused = true;
        

        public void Register(ITickListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Unregister(ITickListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Tick()
        {
            if (_isPaused)
                return;

            var deltaTime = UnityEngine.Time.deltaTime;
            for (var i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].Tick(deltaTime);
            }
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}
