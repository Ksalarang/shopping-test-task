using Shopping.ItemPickUp;
using Shopping.ObjectIds;
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

        [SerializeField]
        private Button _dropItemButton;

        [Header("Configs")]
        [SerializeField]
        private ItemsPickUpConfig _itemsPickUpConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_graphicRaycaster);
            Container.BindInstance(_eventSystem);
            Container.BindInstance(_camera);
            Container.BindInstance(_dropItemButton).WithId(ButtonId.DropItem);

            Container.BindInstance(_itemsPickUpConfig);

            Container.Bind<IMovementInput>().FromInstance(_joystick);

#if UNITY_EDITOR
            Container.BindInterfacesTo<EditorCameraInput>().AsSingle();
#else
            Container.BindInterfacesTo<MobileCameraInput>().AsSingle();
#endif
        }
    }
}