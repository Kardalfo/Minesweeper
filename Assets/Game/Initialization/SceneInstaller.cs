using Game.Camera;
using Game.Gameplay;
using Game.UI.Hud;
using Game.Utils;
using Game.Windows.General;
using Game.Windows.LoseWindow;
using Game.Windows.MenuWindow;
using Game.Windows.PauseWindow;
using Game.Windows.WinWindow;
using UnityEngine;
using Zenject;

namespace Game.Initialization
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private FieldInputController _fieldInputController;
        [SerializeField] private FieldGenerator _fieldGenerator;
        [SerializeField] private WindowSystem _windowSystem;
        [SerializeField] private InitialFieldData _initialFieldData;
        [SerializeField] private HudView _hudView;
        
        
        public override void InstallBindings()
        {
            InstallBootstrap();
            InstallCamera();
            InstallGameplay();
            InstallWindows();
            InstallHud();
            InstallUtils();
        }

        private void InstallCamera()
        {
            Container.Bind<MainCameraController>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void InstallBootstrap()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrap>()
                .AsSingle();
        }

        private void InstallGameplay()
        {
            Container.BindInterfacesAndSelfTo<GameplaySystem>()
                .AsSingle();
            
            Container.Bind<FieldInputController>()
                .FromInstance(_fieldInputController)
                .AsSingle();
            
            Container.Bind<FieldGenerator>()
                .FromInstance(_fieldGenerator)
                .AsSingle();
            
            Container.Bind<FieldFiller>()
                .AsSingle();
            
            Container.Bind<FieldProgressController>()
                .AsSingle();
            
            Container.Bind<InitialFieldData>()
                .FromInstance(_initialFieldData)
                .AsSingle();
        }
        
        private void InstallWindows()
        {
            Container.BindInterfacesAndSelfTo<WindowSystem>()
                .FromComponentInNewPrefab(_windowSystem)
                .AsSingle();

            Container.Bind<IWindowModelProcessor>()
                .To<MenuWindowModelProcessor>()
                .AsSingle();

            Container.Bind<IWindowModelProcessor>()
                .To<WinWindowModelProcessor>()
                .AsSingle();

            Container.Bind<IWindowModelProcessor>()
                .To<LoseWindowModelProcessor>()
                .AsSingle();

            Container.Bind<IWindowModelProcessor>()
                .To<PauseWindowModelProcessor>()
                .AsSingle();
        }

        private void InstallHud()
        {
            Container.BindInterfacesAndSelfTo<HudView>()
                .FromComponentInNewPrefab(_hudView)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<HudModelProcessor>()
                .AsSingle();
        }

        private void InstallUtils()
        {
            Container.BindInterfacesAndSelfTo<Ticker>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<DevRestarter>()
                .AsSingle();
        }
    }
}