using UnityEngine;
using UnityEngine.EventSystems;

namespace Shopping.UserInput
{
    public class Joystick : MonoBehaviour, IMovementInput, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Vector2 Value => _value;

        [SerializeField]
        private RectTransform _handle;

        [SerializeField]
        private RectTransform _background;

        private Vector2 _value;
        private bool _dragging;

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var maxDelta = _background.rect.width / 2f;
            var delta = Vector3.ClampMagnitude((Vector3)eventData.position - _background.position, maxDelta);
            _handle.localPosition = delta;
            _value.x = delta.x / maxDelta;
            _value.y = delta.y / maxDelta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
            _handle.localPosition = Vector3.zero;
            _value = Vector2.zero;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (_dragging)
            {
                return;
            }

            var pressingLeft = Input.GetKey(KeyCode.A);
            var pressingRight = Input.GetKey(KeyCode.D);
            var pressingForward = Input.GetKey(KeyCode.W);
            var pressingBackward = Input.GetKey(KeyCode.S);

            _value.x = pressingLeft == pressingRight ? 0 : pressingRight ? 1 : -1;
            _value.y = pressingForward == pressingBackward ? 0 : pressingForward ? 1 : -1;
        }
#endif
    }
}