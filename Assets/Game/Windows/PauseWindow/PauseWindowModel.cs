using System;
using Cysharp.Threading.Tasks;
using Game.Windows.Models;

namespace Game.Windows.PauseWindow
{
    public class PauseWindowModel : IWindowModel
    {
        public Action RestartAction;
        public Func<UniTask> ContinueAction;
        public Func<UniTask> ExitToMenuAction;
    }
}
