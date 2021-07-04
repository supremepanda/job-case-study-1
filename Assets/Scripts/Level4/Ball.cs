using System;
using UnityEngine;

namespace Level4
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Rigidbody _rigidbody;

        private static float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) 
        {
            return (ballPos.x - racketPos.x) / racketWidth;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Paddle"))
            {
                float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);
                Vector3 direction = new Vector3(x, 0, 1).normalized;
                _rigidbody.velocity = direction * speed;
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(0, 0, -1) * speed;
        }
    }
}
