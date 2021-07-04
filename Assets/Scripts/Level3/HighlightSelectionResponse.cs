using System;
using Interfaces;
using UnityEngine;

namespace Level3
{
    internal class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private Material highlightMaterial;
        private CandyCrushGameControl _gameControl;

        private void Start()
        {
            _gameControl = FindObjectOfType<CandyCrushGameControl>();
        }

        public void OnSelect(Transform selection)
        {
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                var food = selectionRenderer.gameObject.GetComponent<Food>();
                food.isSelected = true;
                var materials = selectionRenderer.materials;
                materials[food.TargetMaterialIndex] = this.highlightMaterial;
                selectionRenderer.materials = materials;

            }
        }

        public void OnDeselect(Transform selection)
        {
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                var food = selectionRenderer.gameObject.GetComponent<Food>();
                food.isSelected = false;
                var materials = selectionRenderer.materials;
                materials[food.TargetMaterialIndex] = food.DefaultMaterial;
                selectionRenderer.materials = materials;
            }
        }
    }
}