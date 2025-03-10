using System;
using Shopping.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Shopping.UserInput
{
    public class EditorCameraInput : ICameraInput, ITickable
    {
        public event Action<Collider> OnClick;
        public Vector2 Delta => _delta;

        [Inject]
        private GraphicRaycaster _graphicRaycaster;

        [Inject]
        private EventSystem _eventSystem;

        [Inject]
        private Camera _camera;

        private Vector2 _delta;
        private Vector3 _prevMousePosition;
        private bool _isMouseDown;
        private bool _wasMouseDown;
        private bool _overUi;
        private bool _moved;

        public void Tick()
        {
            _wasMouseDown = _isMouseDown;
            _isMouseDown = Input.GetMouseButton(0);

            if (_isMouseDown)
            {
                var mousePosition = Input.mousePosition;

                if (_wasMouseDown == false) // began phase
                {
                    _prevMousePosition = mousePosition;
                    var results = GraphicUtils.Raycast(_graphicRaycaster, _eventSystem, mousePosition);
                    _overUi = results.Count > 0;
                }
                else // moved/stationary phase
                {
                    if (_overUi == false)
                    {
                        _delta = mousePosition - _prevMousePosition;
                        _prevMousePosition = mousePosition;

                        if (_delta != Vector2.zero)
                        {
                            _moved = true;
                        }
                    }
                }
            }
            else
            {
                if (_wasMouseDown) // ended phase
                {
                    if ((_moved || _overUi) == false)
                    {
                        OnClick?.Invoke(PhysicsUtils.Raycast(_camera, Input.mousePosition));
                    }

                    _delta = Vector2.zero;
                    _overUi = false;
                    _moved = false;
                }
            }
        }
    }
}