using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay
{
    public class FieldInputController : MonoBehaviour
    {
        public event Action<Vector2Int> CellPressedEvent;
        public event Action<Vector2Int> MarkRequestedEvent;
        public event Action RestartRequestedEvent;
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private UnityEngine.Camera _camera;
        
        private bool _isActive;
        
        
        public void Update()
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartRequestedEvent?.Invoke();
            }

            if (_isActive == false)
                return;
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                HandleLeftClick();
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                HandleRightClick();
            }
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        private void HandleLeftClick()
        {
            if (TryGetCellUnderMouse(out var cell))
            {
                CellPressedEvent?.Invoke(cell.GridPosition);
            }
        }

        private void HandleRightClick()
        {
            if (TryGetCellUnderMouse(out var cell))
            {
                MarkRequestedEvent?.Invoke(cell.GridPosition);
            }
        }

        private bool TryGetCellUnderMouse(out FieldCellView cellView)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _layerMask);
            if (hit == false)
            {
                cellView = null;
                return false;
            }

            return hit.collider.TryGetComponent(out cellView);
        }
        
        public void Clear()
        {
            CellPressedEvent = null;
            MarkRequestedEvent = null;
            RestartRequestedEvent = null;
        }
    }
}