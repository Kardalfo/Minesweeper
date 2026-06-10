using System;
using Game.Windows.Models;

namespace Game.Windows.General
{
    public abstract class WindowModelProcessorBase<TModel> : IWindowModelProcessor<TModel>
        where TModel : class, IWindowModel
    {
        public Type GetModelType()
        {
            return typeof(TModel);
        }

        IWindowModel IWindowModelProcessor.GetModel()
        {
            return GetModel();
        }

        
        public abstract TModel GetModel();
    }
}