using System;
using UnityEngine;

namespace Level4
{
    public class Paddle : MonoBehaviour
    {
        private const float PaddleRange = 2.63f;

        private void Update()
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            float wordlPos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(pos).x, -2.63f, 2.63f);

            transform.position = new Vector3(wordlPos, 0f, -5f);
        }
    }
}
