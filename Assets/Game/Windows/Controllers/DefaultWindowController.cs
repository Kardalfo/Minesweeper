using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Windows.Controllers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DefaultWindowController : WindowController
    {
        [SerializeField] private float _openDuration;
        [SerializeField] private AnimationCurve _openCurve;
        [SerializeField] private float _closeDuration;
        [SerializeField] private AnimationCurve _closeCurve;

        private CanvasGroup _canvasGroup;
        private Sequence _animationSequence;
        
        
        protected override void Awake()
        {
            base.Awake();
            
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override UniTask AnimateOpen()
        {
            SetInteractable(false);

            _canvasGroup.alpha = 0;
            
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1, _openDuration)).SetEase(_openCurve)
                .OnComplete(() =>
                {
                    SetInteractable(true);
                });

            return _animationSequence.AsyncWaitForCompletion().AsUniTask();
        }

        protected override UniTask AnimateClose()
        {
            SetInteractable(false);
            
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0, _closeDuration))
                .SetEase(_closeCurve)
                .OnComplete(() =>
                {
                    SetInteractable(true);
                });
            
            return _animationSequence.AsyncWaitForCompletion().AsUniTask();
        }
        
        private void SetInteractable(bool isInteractable)
        {
            _canvasGroup.interactable = isInteractable;
        }
    }
}