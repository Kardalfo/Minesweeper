using System;
using Cysharp.Threading.Tasks;
using Game.Windows.General;
using UnityEngine;

namespace Game.Windows.Controllers
{
    [RequireComponent(typeof(Canvas), typeof(RectTransform))]
    public abstract class WindowController : MonoBehaviour
    {
        public Type WindowType { get; private set; }
        public Canvas Canvas { get; private set; }
        
        protected RectTransform RectTransform;
        

        protected virtual void Awake()
        {
            WindowType = GetComponent<WindowBase>().GetType();
            Canvas = GetComponent<Canvas>();
            
            RectTransform = GetComponent<RectTransform>();
        }

        protected virtual void OnDestroy()
        {
            Canvas = null;
        }

        public virtual void Adjust()
        {
            AdjustEdges();
            AdjustParameters();
        }

        private void AdjustEdges()
        {
            RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
            RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        }

        private void AdjustParameters()
        {
            RectTransform.localScale = Vector3.one;
            
            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
            
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
        
        public async UniTask Open()
        {
            await AnimateOpen();

            HandleOpened();
        }

        protected virtual UniTask AnimateOpen()
        {
            return UniTask.CompletedTask;
        }

        private void HandleOpened()
        {
            Debug.Log("Window opened");
        }
        
        public async UniTask Close()
        {
            await AnimateClose();

            HandleClosed();
        }

        protected virtual UniTask AnimateClose()
        {
            return UniTask.CompletedTask;
        }

        private void HandleClosed()
        {
            Debug.Log("Window closed");
        }
    }
}