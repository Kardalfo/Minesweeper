using Game.Utils;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class HudTimer : MonoBehaviour, ITickListener
    {
        [SerializeField] private TMP_Text _timeTmp;
        
        private float _timeValue;


        private void Awake()
        {
            SetTime(0);
        }

        public void Tick(float deltaTime)
        {
            _timeValue += deltaTime;
            SetTime(_timeValue);
        }

        private void SetTime(float time)
        {
            _timeTmp.text = time.ToString("0.00");
        }

        public void ResetTimer()
        {
            _timeValue = 0;
            SetTime(0);
        }
    }
}
