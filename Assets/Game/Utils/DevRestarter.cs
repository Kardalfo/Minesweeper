using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Windows.General;
using Zenject;

namespace Game.Utils
{
    public class DevRestarter : IInitializable
    {
        private readonly WindowSystem _windowSystem;
        private readonly GameplaySystem _gameplaySystem;
        private readonly FieldInputController _inputController;
        
        
        public DevRestarter(WindowSystem windowSystem, GameplaySystem gameplaySystem, FieldInputController inputController)
        {
            _windowSystem = windowSystem;
            _gameplaySystem = gameplaySystem;
            _inputController = inputController;
        }

        public void Initialize()
        {
            _gameplaySystem.GamePreparedEvent += HandleGamePrepared;
        }

        private void Restart()
        {
            _windowSystem.CloseAll().Forget();
            
            _gameplaySystem.Restart();
        }

        private void HandleGamePrepared()
        {
            _inputController.RestartRequestedEvent -= Restart;
            _inputController.RestartRequestedEvent += Restart;
        }
    }
}