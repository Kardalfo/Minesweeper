using System;
using Cysharp.Threading.Tasks;
using Game.Windows.Models;

namespace Game.Windows.WinWindow
{
    public class WinWindowModel : IWindowModel
    {
        public Action RestartAction;
        public Func<UniTask> ExitToMenuAction;
    }
}
