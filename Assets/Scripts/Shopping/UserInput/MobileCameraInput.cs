using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Shopping.UserInput
{
    [UsedImplicitly]
    public class MobileCameraInput : ICameraInput, ITickable
    {
        public Vector2 Delta => _delta;

        private Vector2 _delta;

        public void Tick()
        {
            ;
        }
    }
}