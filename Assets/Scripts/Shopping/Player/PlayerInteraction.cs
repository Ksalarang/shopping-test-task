using Shopping.ItemPickUp;
using Shopping.UserInput;
using UnityEngine;
using Zenject;

namespace Shopping.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Inject]
        private ICameraInput _cameraInput;

        private void Awake()
        {
            _cameraInput.OnClick += OnClick;
        }

        private void OnDestroy()
        {
            _cameraInput.OnClick -= OnClick;
        }

        private void OnClick(Collider other)
        {
            if (other && other.TryGetComponent(out Item item))
            {
                ;
            }
        }
    }
}