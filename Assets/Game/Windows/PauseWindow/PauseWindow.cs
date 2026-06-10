using System;
using Cysharp.Threading.Tasks;
using Game.Windows.General;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows.PauseWindow
{
    public class PauseWindow : Window<PauseWindowModel>
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitToMenuButton;

        private Action _restartAction;
        private Func<UniTask> _continueAction;
        private Func<UniTask> _exitToMenuAction;

        
        protected override void Setup(PauseWindowModel model)
        {
            _restartAction = model.RestartAction;
            _continueAction = model.ContinueAction;
            _exitToMenuAction = model.ExitToMenuAction;

            _restartButton.onClick.AddListener(Restart);
            _continueButton.onClick.AddListener(Continue);
            _exitToMenuButton.onClick.AddListener(ExitToMenu);
        }

        private void Restart()
        {
            _restartAction?.Invoke();
        }

        private void Continue()
        {
            _continueAction?.Invoke().Forget();
        }

        private void ExitToMenu()
        {
            _exitToMenuAction?.Invoke().Forget();
        }
    }
}
