using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Level1
{
    public class Olive : MonoBehaviour
    {
        [SerializeField] private Material collectedColorMaterial;
        
        private MeshRenderer _meshRenderer;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private Progress _progress;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            _progress = FindObjectOfType<Progress>();
        }

        public void Collected()
        {
            _meshRenderer.material = collectedColorMaterial;
            _collider.enabled = false;
            _rigidbody.velocity = Vector3.zero;
            _progress.IncreaseProgress();
        }
    }
}
