using System;
using Game.Windows.Models;
using UnityEngine;

namespace Game.Windows.General
{
    public abstract class WindowBase : MonoBehaviour
    {
        public abstract Type GetModelType();
        public abstract void Setup(IWindowModel model);
    }
}