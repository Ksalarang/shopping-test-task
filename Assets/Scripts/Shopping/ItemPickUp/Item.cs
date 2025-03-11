using UnityEngine;

namespace Shopping.ItemPickUp
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField]
        public Rigidbody Rigidbody { get; private set; }
    }
}