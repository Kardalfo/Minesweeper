using System;
using Cysharp.Threading.Tasks;
using Game.Windows.Models;

namespace Game.Windows.LoseWindow
{
    public class LoseWindowModel : IWindowModel
    {
        public Action RestartAction;
        public Func<UniTask> ExitToMenuAction;
    }
}
