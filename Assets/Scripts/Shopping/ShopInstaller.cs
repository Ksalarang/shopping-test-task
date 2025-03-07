using Shopping.UserInput;
using UnityEngine;
using Zenject;

namespace Shopping
{
    public class ShopInstaller : MonoInstaller
    {
        [Header("Scene objects")]
        [SerializeField]
        private Joystick _joystick;

        public override void InstallBindings()
        {
            Container.Bind<IMovementInput>().FromInstance(_joystick);
        }
    }
}