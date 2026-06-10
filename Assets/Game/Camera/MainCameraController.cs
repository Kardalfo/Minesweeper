using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Game.Camera
{
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        
        [Inject]
        private void Construct(IEnumerable<IAdditionalCameraProvider> providers)
        {
            if (providers == null)
                return;
            
            var cameraData = _camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Clear();

            var additionalCameraProviders = providers as IAdditionalCameraProvider[] ?? providers.ToArray();
            var cameras = additionalCameraProviders
                .OrderBy(p => p.CameraData.Priority)
                .Select(p => p.CameraData.Camera);
            foreach (var additionalCamera in cameras)
            {
                cameraData.cameraStack.Add(additionalCamera);
            }
        }
    }
}
