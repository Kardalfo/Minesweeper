using Game.UI;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Windows.General;
using Game.Windows.PauseWindow;

using Game.Utils;
using Zenject;

namespace Game.UI.Hud
{
    public class HudModelProcessor : IInitializable
    {
        private readonly WindowSystem _windowSystem;
        private readonly GameplaySystem _gameplaySystem;
        private readonly Ticker _ticker;
        private readonly HudView _view;

        
        public HudModelProcessor(WindowSystem windowSystem, GameplaySystem gameplaySystem, Ticker ticker, HudView view)
        {
            _windowSystem = windowSystem;
            _gameplaySystem = gameplaySystem;
            _ticker = ticker;
            _view = view;
        }

        public void Initialize()
        {
            var model = new HudModel
            {
                PauseAction = OnPausePressed,
                RegisterTickListener = _ticker.Register,
                UnregisterTickListener = _ticker.Unregister,
            };
            
            _view.Setup(model);
        }

        private void OnPausePressed()
        {
            _gameplaySystem.Pause();
            _windowSystem.Open<PauseWindow>().Forget();
        }
    }
}
