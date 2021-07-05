using System;
using Interfaces;
using UnityEngine;

namespace Level3
{
    internal class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private Material highlightMaterial;
        
        /// <summary>
        /// Select food and change its material to highlight material.
        /// </summary>
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

        /// <summary>
        /// Deselect food and change its material to default material.
        /// </summary>
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