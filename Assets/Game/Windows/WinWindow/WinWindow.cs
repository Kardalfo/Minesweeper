using System;
using Cysharp.Threading.Tasks;
using Game.Windows.General;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows.WinWindow
{
    public class WinWindow : Window<WinWindowModel>
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitToMenuButton;

        private Action _restartAction;
        private Func<UniTask> _exitToMenuAction;

        protected override void Setup(WinWindowModel model)
        {
            _restartAction = model.RestartAction;
            _exitToMenuAction = model.ExitToMenuAction;

            _restartButton.onClick.AddListener(Restart);
            _exitToMenuButton.onClick.AddListener(ExitToMenu);
        }

        private void Restart()
        {
            _restartAction?.Invoke();
        }

        private void ExitToMenu()
        {
            _exitToMenuAction?.Invoke().Forget();
        }
    }
}
