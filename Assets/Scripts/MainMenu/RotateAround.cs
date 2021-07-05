using System;
using UnityEngine;

namespace MainMenu
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        private void Update()
        {
            transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
        }
    }
}
