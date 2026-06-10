using Cysharp.Threading.Tasks;
using Game.UI.Hud;
using Game.Windows.General;
using Game.Windows.MenuWindow;
using Zenject;

namespace Game
{
    public class GameBootstrap : IInitializable
    {
        private readonly WindowSystem _windowSystem;
        private readonly HudModelProcessor _hudModelProcessor;
        
        
        public GameBootstrap(WindowSystem windowSystem, HudModelProcessor hudModelProcessor)
        {
            _windowSystem = windowSystem;
            _hudModelProcessor = hudModelProcessor;
        }
        
        public void Initialize()
        {
            _hudModelProcessor.Initialize();
            
            _windowSystem.Open<MenuWindow>().Forget();
        }
    }
}