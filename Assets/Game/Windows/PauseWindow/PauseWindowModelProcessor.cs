using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Windows.General;

namespace Game.Windows.PauseWindow
{
    public class PauseWindowModelProcessor : WindowModelProcessorBase<PauseWindowModel>
    {
        private readonly WindowSystem _windowSystem;
        private readonly GameplaySystem _gameplaySystem;

        
        public PauseWindowModelProcessor(WindowSystem windowSystem, GameplaySystem gameplaySystem)
        {
            _windowSystem = windowSystem;
            _gameplaySystem = gameplaySystem;
        }

        public override PauseWindowModel GetModel()
        {
            return new PauseWindowModel
            {
                RestartAction = Restart,
                ContinueAction = Continue,
                ExitToMenuAction = ExitToMenu,
            };
        }

        private void Restart()
        {
            _gameplaySystem.Restart();
            _windowSystem.Close<PauseWindow>().Forget();
        }

        private async UniTask Continue()
        {
            await _windowSystem.Close<PauseWindow>();
            _gameplaySystem.Unpause();
        }

        private async UniTask ExitToMenu()
        {
            _gameplaySystem.Reset();
            
            await _windowSystem.Close<PauseWindow>();
            
            _windowSystem.Open<MenuWindow.MenuWindow>().Forget();
        }
    }
}
