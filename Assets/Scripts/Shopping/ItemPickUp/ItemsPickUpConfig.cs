using UnityEngine;

namespace Shopping.ItemPickUp
{
    [CreateAssetMenu(fileName = "ItemsPickUpConfig", menuName = "Configs/ItemsPickUpConfig", order = 0)]
    public class ItemsPickUpConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector3 ItemPosition { get; private set; }

        [field: SerializeField]
        public float DropForce { get; private set; }

        [field: SerializeField]
        public float MaxDistance { get; private set; }
    }
}