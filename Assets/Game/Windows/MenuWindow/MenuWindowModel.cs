using System;
using Cysharp.Threading.Tasks;
using Game.Windows.Models;

namespace Game.Windows.MenuWindow
{
    public class MenuWindowModel : IWindowModel
    {
        public Func<UniTask> StartGameAction;
    }
}