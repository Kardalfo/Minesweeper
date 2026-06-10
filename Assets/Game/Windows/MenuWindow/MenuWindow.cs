using System;
using Cysharp.Threading.Tasks;
using Game.Windows.General;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows.MenuWindow
{
    public class MenuWindow : Window<MenuWindowModel>
    {
        [SerializeField] private Button _startButton;

        private Func<UniTask> _startGameAction;


        protected override void Setup(MenuWindowModel model)
        {
            _startGameAction = model.StartGameAction;

            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _startGameAction?.Invoke().Forget();
        }
    }
}