using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Windows.General;
using Game.Windows.MenuWindow;

namespace Game.Windows.WinWindow
{
    public class WinWindowModelProcessor : WindowModelProcessorBase<WinWindowModel>
    {
        private readonly WindowSystem _windowSystem;
        private readonly GameplaySystem _gameplaySystem;

        public WinWindowModelProcessor(WindowSystem windowSystem, GameplaySystem gameplaySystem)
        {
            _windowSystem = windowSystem;
            _gameplaySystem = gameplaySystem;
        }

        public override WinWindowModel GetModel()
        {
            return new WinWindowModel
            {
                RestartAction = Restart,
                ExitToMenuAction = ExitToMenu,
            };
        }

        private void Restart()
        {
            _gameplaySystem.Restart();
            _windowSystem.Close<WinWindow>().Forget();
        }

        private async UniTask ExitToMenu()
        {
            _gameplaySystem.Reset();
            
            await _windowSystem.Close<WinWindow>();
            
            _windowSystem.Open<MenuWindow.MenuWindow>().Forget();
        }
    }
}
