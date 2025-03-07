using Shopping.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Shopping.UserInput
{
    public class EditorCameraInput : ICameraInput, ITickable
    {
        public Vector2 Delta => _delta;

        [Inject]
        private GraphicRaycaster _graphicRaycaster;

        [Inject]
        private EventSystem _eventSystem;

        private Vector2 _delta;
        private Vector3 _prevMousePosition;
        private bool _isMouseDown;
        private bool _wasMouseDown;
        private bool _overUi;

        public void Tick()
        {
            _wasMouseDown = _isMouseDown;
            _isMouseDown = Input.GetMouseButton(0);

            if (_isMouseDown)
            {
                var mousePosition = Input.mousePosition;
                if (_wasMouseDown == false)
                {
                    _prevMousePosition = mousePosition;
                    var results = GraphicUtils.Raycast(_graphicRaycaster, _eventSystem, mousePosition);
                    _overUi = results.Count > 0;
                }
                else
                {
                    if (_overUi == false)
                    {
                        _delta = mousePosition - _prevMousePosition;
                        _prevMousePosition = mousePosition;
                    }
                }
            }
            else
            {
                if (_wasMouseDown)
                {
                    _delta = Vector2.zero;
                    _overUi = false;
                }
            }
        }
    }
}