using UnityEngine;

namespace Shopping.Utils
{
    public class FpsSetter : MonoBehaviour
    {
        [SerializeField]
        private int _targetFrameRate;

        private void Start()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}