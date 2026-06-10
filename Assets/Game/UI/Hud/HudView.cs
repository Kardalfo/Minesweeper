using System;
using Game.Camera;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Hud
{
    public class HudView : MonoBehaviour, IAdditionalCameraProvider
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private HudTimer _timer;
        [SerializeField] private UnityEngine.Camera _camera;
        
        private Action _pauseAction;
        private Action<ITickListener> _unregisterTickListenerAction;
        

        public AdditionalCameraData CameraData => new()
        {
            Camera = _camera,
            Priority = 5,
        };

        private void OnDestroy()
        {
            _unregisterTickListenerAction?.Invoke(_timer);
        }
        
        public void Setup(HudModel model)
        {
            _unregisterTickListenerAction = model.UnregisterTickListener;
            _pauseAction = model.PauseAction;
            _pauseButton.onClick.AddListener(Pause);
            
            model.RegisterTickListener?.Invoke(_timer);
        }

        private void Pause()
        {
            _pauseAction?.Invoke();
        }

        public void ResetHud()
        {
            _timer.ResetTimer();
        }
    }
}
