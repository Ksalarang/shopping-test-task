using UnityEngine;
using UnityEngine.EventSystems;

namespace Shopping.UserInput
{
    public class Joystick : MonoBehaviour, IMovementInput, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Vector2 Value => _inputVector;

        [SerializeField]
        private RectTransform _handle;

        [SerializeField]
        private RectTransform _background;

        private Vector2 _inputVector;

        public void OnPointerDown(PointerEventData eventData) {}

        public void OnDrag(PointerEventData eventData)
        {
            var delta = (Vector3)eventData.position - _background.position;
            delta = Vector3.ClampMagnitude(delta, _background.rect.width / 2f);
            _handle.localPosition = delta;
            _inputVector = delta.normalized;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.localPosition = Vector3.zero;
            _inputVector = Vector2.zero;
        }
    }
}