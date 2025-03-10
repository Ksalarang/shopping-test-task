using UnityEngine;

namespace Shopping.Utils
{
    public static class PhysicsUtils
    {
        public static Collider Raycast(Camera camera, Vector3 screenPosition)
        {
            Physics.Raycast(camera.ScreenPointToRay(screenPosition), out var hit);
            return hit.collider;
        }
    }
}