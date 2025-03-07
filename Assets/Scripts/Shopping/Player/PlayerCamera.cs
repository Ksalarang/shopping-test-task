using Shopping.UserInput;
using Shopping.Utils;
using UnityEngine;
using Zenject;

namespace Shopping.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Inject]
        private ICameraInput _cameraInput;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _sensitivity = 10f;

        [SerializeField]
        private float _maxCameraDelta = 10f;

        private void Update()
        {
            var rotationAngles = transform.eulerAngles;
            var cameraDelta = _cameraInput.Delta;
            var speedVector = cameraDelta * (_sensitivity * Time.deltaTime);

            if (cameraDelta != Vector2.zero)
            {
                speedVector = MathUtils.ClampAbsoluteValue(speedVector, 0f, _maxCameraDelta);
            }

            speedVector.y = -speedVector.y;

            rotationAngles.y += speedVector.x;
            _rigidbody.MoveRotation(Quaternion.Euler(rotationAngles));
            _rigidbody.angularVelocity = Vector3.zero;

            var nextXAngle = _camera.transform.eulerAngles.x + speedVector.y;

            switch (nextXAngle)
            {
                case <= 90f and >= 80f:
                case >= 270f and <= 280f:
                    speedVector.y = 0f;
                    break;
            }

            _camera.transform.Rotate(Vector3.right, speedVector.y);
        }
    }
}