using System;
using Common;
using UnityEngine;

namespace Level3
{
    public class Food : MonoBehaviour
    {
        public CandyCrushFoodType foodType;
        private Material _defaultMaterial;
        public Material DefaultMaterial => _defaultMaterial;

        [SerializeField] private int targetMaterialIndex;
        public int TargetMaterialIndex => targetMaterialIndex;

        public bool isSelected = false;

        // Index on FoodGameArea 2D array.
        public (int, int) index2DArray;
        
        private void Start()
        {
            _defaultMaterial = GetComponent<MeshRenderer>().materials[targetMaterialIndex];
        }
    }
}