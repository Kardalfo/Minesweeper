using System;
using Game.Windows.Models;

namespace Game.Windows.General
{
    public interface IWindowModelProcessor<out TModel> : IWindowModelProcessor
        where TModel : class, IWindowModel
    {
        public new TModel GetModel();
    }
    
    public interface IWindowModelProcessor
    {
        public Type GetModelType();
        public IWindowModel GetModel();
    }
}