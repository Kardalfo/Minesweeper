using System;

using Game.Utils;

namespace Game.UI.Hud
{
    public class HudModel
    {
        public Action PauseAction;
        public Action<ITickListener> RegisterTickListener;
        public Action<ITickListener> UnregisterTickListener;
    }
}
