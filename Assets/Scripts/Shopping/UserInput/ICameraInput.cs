using System;
using UnityEngine;

namespace Shopping.UserInput
{
    public interface ICameraInput
    {
        event Action<Collider> OnClick;

        Vector2 Delta { get; }
    }
}