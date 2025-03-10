using Shopping.UserInput;
using UnityEngine;
using Zenject;

namespace Shopping.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject]
        private IMovementInput _movementInput;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _speed = 10f;

        private void Update()
        {
            var cameraTransform = _camera.transform;
            var movementInput = _movementInput.Value;
            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;

            cameraForward.y = cameraRight.y = 0;
            var direction = cameraForward * movementInput.y + cameraRight * movementInput.x;

            _rigidbody.MovePosition(transform.position + direction * (_speed * Time.deltaTime));
        }
    }
}