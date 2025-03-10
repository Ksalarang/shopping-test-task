using Shopping.UserInput;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Shopping
{
    public class ShopInstaller : MonoInstaller
    {
        [Header("Scene objects")]
        [SerializeField]
        private Joystick _joystick;

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private Camera _camera;

        public override void InstallBindings()
        {
            Container.BindInstance(_graphicRaycaster);
            Container.BindInstance(_eventSystem);
            Container.BindInstance(_camera);

            Container.Bind<IMovementInput>().FromInstance(_joystick);

#if UNITY_EDITOR
            Container.BindInterfacesTo<EditorCameraInput>().AsSingle();
#else
            Container.BindInterfacesTo<MobileCameraInput>().AsSingle();
#endif
        }
    }
}