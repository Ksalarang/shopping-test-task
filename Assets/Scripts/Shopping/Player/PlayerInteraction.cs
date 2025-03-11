using Shopping.ItemPickUp;
using Shopping.ObjectIds;
using Shopping.UserInput;
using Shopping.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shopping.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Inject]
        private ICameraInput _cameraInput;

        [Inject]
        private ItemsPickUpConfig _itemsPickUpConfig;

        [Inject(Id = ButtonId.DropItem)]
        private Button _dropItemButton;

        private Item _currentItem;

        private void Awake()
        {
            _cameraInput.OnClick += OnClick;

            _dropItemButton.gameObject.SetActive(false);
            _dropItemButton.onClick.AddListener(DropItem);
        }

        private void OnDestroy()
        {
            _cameraInput.OnClick -= OnClick;
            _dropItemButton.onClick.RemoveListener(DropItem);
        }

        private void OnClick(Collider other)
        {
            if (other && other.TryGetComponent(out Item item))
            {
                if (_currentItem is not null
                    || Vector3.Distance(transform.position, item.transform.position) > _itemsPickUpConfig.MaxDistance)
                {
                    return;
                }

                var itemTransform = item.transform;
                var itemScale = itemTransform.localScale;

                itemTransform.parent = transform;
                itemTransform.localScale = MathUtils.DivideComponents(itemScale, transform.localScale);
                itemTransform.localPosition = _itemsPickUpConfig.ItemPosition;
                itemTransform.localRotation = Quaternion.identity;
                item.Rigidbody.isKinematic = true;
                _currentItem = item;
                _dropItemButton.gameObject.SetActive(true);
            }
        }

        private void DropItem()
        {
            var itemScale = _currentItem.transform.localScale;
            _currentItem.transform.parent = null;
            _currentItem.transform.localScale = MathUtils.MultiplyComponents(itemScale, transform.localScale);
            _currentItem.Rigidbody.isKinematic = false;
            _currentItem.Rigidbody.AddForce(transform.forward * _itemsPickUpConfig.DropForce, ForceMode.Impulse);
            _currentItem = null;
            _dropItemButton.gameObject.SetActive(false);
        }
    }
}