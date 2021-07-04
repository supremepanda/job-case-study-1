using System;
using Common;
using UnityEngine;

namespace Level4
{
    public class Paddle : MonoBehaviour
    {
        private const float PaddleRange = 2.63f;
        public int direction = 1;

        private ApplicationType _applicationType;

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

        private void Start()
        {
            _applicationType = FindObjectOfType<GameManager>().ApplicationType;
        }

        private void Update()
        {
            if (_applicationType == ApplicationType.Editor)
            {
                PaddleMovement(Input.mousePosition);
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        PaddleMovement(touch.position);
                    }
                }
            }
        }

        private void PaddleMovement(Vector3 position)
        {
            Vector3 pos = position;
            pos.z = 10;
            float wordlPos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(pos).x, -2.63f, 2.63f);

            transform.position = new Vector3(wordlPos * direction, 0f, -5f);
        }
    }
}
