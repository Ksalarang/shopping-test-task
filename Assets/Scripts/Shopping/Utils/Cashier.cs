using UnityEngine;

namespace Shopping.Utils
{
    public class Cashier : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Transform _player;

        private void Update()
        {
            _rigidbody.MoveRotation(Quaternion.LookRotation((transform.position - _player.position).normalized));
        }
    }
}