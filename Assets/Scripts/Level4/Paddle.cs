using System;
using Common;
using UnityEngine;

namespace Level4
{
    public class Paddle : MonoBehaviour
    {
        private const float PaddleRange = 2.63f;
        public int direction = 1;

        public void PaddleLengthUp()
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(Level4Constants.PaddleLengthUp, localScale.y,
                localScale.z);
            transform1.localScale = localScale;
        }

        public void PaddleLengthDown()
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(Level4Constants.PaddleLengthDown, localScale.y,
                localScale.z);
            transform1.localScale = localScale;
        }

        private void Update()
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            float wordlPos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(pos).x, -2.63f, 2.63f);

            transform.position = new Vector3(wordlPos * direction, 0f, -5f);
        }
    }
}
