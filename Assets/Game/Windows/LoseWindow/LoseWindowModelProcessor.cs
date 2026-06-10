using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Windows.General;
using Game.Windows.MenuWindow;

namespace Game.Windows.LoseWindow
{
    public class LoseWindowModelProcessor : WindowModelProcessorBase<LoseWindowModel>
    {
        private readonly WindowSystem _windowSystem;
        private readonly GameplaySystem _gameplaySystem;

        public LoseWindowModelProcessor(WindowSystem windowSystem, GameplaySystem gameplaySystem)
        {
            _windowSystem = windowSystem;
            _gameplaySystem = gameplaySystem;
        }

        public override LoseWindowModel GetModel()
        {
            return new LoseWindowModel
            {
                RestartAction = Restart,
                ExitToMenuAction = ExitToMenu,
            };
        }

        private void Restart()
        {
            _gameplaySystem.Restart();
            
            _windowSystem.Close<LoseWindow>().Forget();
        }

        private async UniTask ExitToMenu()
        {
            _gameplaySystem.Reset();
            
            await _windowSystem.Close<LoseWindow>();
            
            _windowSystem.Open<MenuWindow.MenuWindow>().Forget();
        }
    }
}
