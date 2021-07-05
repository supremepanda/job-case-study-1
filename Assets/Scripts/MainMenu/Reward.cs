using System;
using UnityEngine;

namespace MainMenu
{
    public class Reward : MonoBehaviour
    {
        private const float ReferenceWidth = 289f;
        private const float ReferenceHeight = 513f;
        private Vector3 _currentScale;

        private void Start()
        {
            var transform1 = transform;
            _currentScale = transform1.localScale;
            _currentScale *= Screen.width / ReferenceWidth;
            transform1.localScale = _currentScale;
        }
    }
}
