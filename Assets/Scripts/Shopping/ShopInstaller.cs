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

        public override void InstallBindings()
        {
            Container.BindInstance(_graphicRaycaster);
            Container.BindInstance(_eventSystem);

            Container.Bind<IMovementInput>().FromInstance(_joystick);

#if UNITY_EDITOR
            Container.BindInterfacesTo<EditorCameraInput>().AsSingle();
#else
            Container.BindInterfacesTo<MobileCameraInput>().AsSingle();
#endif
        }
    }
}