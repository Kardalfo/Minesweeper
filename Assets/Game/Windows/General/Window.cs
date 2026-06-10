using System;
using Game.Windows.Models;
using UnityEngine;

namespace Game.Windows.General
{
    public abstract class Window<TModel> : WindowBase
        where TModel : class, IWindowModel
    {
        public override Type GetModelType()
        {
            return typeof(TModel);
        }

        public override void Setup(IWindowModel model)
        {
            if (model is not TModel windowModel)
            {
                Debug.LogError($"Model {model.GetType().Name} is not {typeof(TModel).Name}");
                return;
            }
            
            Setup(windowModel);
        }
        
        
        protected abstract void Setup(TModel model);
    }
}