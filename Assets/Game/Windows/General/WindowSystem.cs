using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Camera;
using Game.Windows.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Windows.General
{
    [RequireComponent(typeof(Canvas))]
    public class WindowSystem : MonoBehaviour, IAdditionalCameraProvider
    {
        [SerializeField] private List<WindowBase> _windowPrefabs;
        [SerializeField] private Canvas _canvas;
        
        private readonly Dictionary<Type, WindowBase> _windowPrefabsByType = new();
        private readonly Dictionary<Type, WindowController> _currentControllersByType = new();
        
        private Dictionary<Type, IWindowModelProcessor> _modelProcessorsByType = new();
        
        private int _nextWindowSortingOrder;

        
        public AdditionalCameraData CameraData => new()
        {
            Camera = _canvas.worldCamera,
            Priority = 10,
        };

        private void Awake()
        {
            foreach (var windowPrefab in _windowPrefabs)
            {
                _windowPrefabsByType.Add(windowPrefab.GetType(), windowPrefab);
            }
        }

        [Inject]
        public void Construct(IEnumerable<IWindowModelProcessor> modelProcessors)
        {
            _modelProcessorsByType = modelProcessors
                .ToDictionary(p => p.GetModelType(), p => p);
        }
        
        public async UniTask Open<TWindow>()
            where TWindow : WindowBase
        {
            var windowType = typeof(TWindow);
            if (_windowPrefabsByType.ContainsKey(windowType) == false)
            {
                Debug.LogError($"Window {windowType.Name} not found in prefabs");
                return;
            }

            if (_currentControllersByType.ContainsKey(windowType) == false)
            {
                var tWindow = CreateWindow<TWindow>(windowType);
                var modelType = tWindow.GetModelType();
                if (_modelProcessorsByType.TryGetValue(modelType, out var processor) == false)
                {
                    Debug.LogError($"Model processor for {windowType.Name} not found");
                    return;
                }
                
                var model = processor.GetModel();
                tWindow.Setup(model);
                var controller = _currentControllersByType[windowType];

                SetWindowSorting(controller);

                await controller.Open();
            }
        }

        public async UniTask Close<TWindow>()
        {
            var windowType = typeof(TWindow);
            
            await Close(windowType);
        }

        private async UniTask Close(Type windowType)
        {
            if (_currentControllersByType.TryGetValue(windowType, out var windowController) == false)
            {
                Debug.LogWarning($"Window {windowType.Name} not opened");
                return;
            }

            await windowController.Close();
                    
            Destroy(windowController.gameObject);
                    
            _currentControllersByType.Remove(windowType);
                    
            CheckForAllWindowsClosed();
        }

        public async UniTask CloseAll()
        {
            foreach (var windowController in _currentControllersByType.Values.ToArray())
            {
                await Close(windowController.WindowType);
            }
        }

        private WindowBase CreateWindow<TWindow>(Type windowType)
            where TWindow : WindowBase
        {
            var original = _windowPrefabsByType[windowType];
            var window = Instantiate((TWindow)original, transform);

            var controller = window.GetComponent<WindowController>();
            controller.Adjust();

            _currentControllersByType.Add(windowType, controller);

            return window;
        }

        private void SetWindowSorting(WindowController windowController)
        {
            windowController.gameObject.layer = gameObject.layer;

            var canvas = windowController.Canvas;
            
            canvas.overrideSorting = true;
            canvas.sortingLayerID = _canvas.sortingLayerID;
            canvas.sortingOrder = _nextWindowSortingOrder;
            
            _nextWindowSortingOrder++;
        }

        private void CheckForAllWindowsClosed()
        {
            if (_currentControllersByType.Count == 0)
            {
                _nextWindowSortingOrder = 0;
            }
        }
    }
}