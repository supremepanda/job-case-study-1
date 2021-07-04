using System;
using UnityEngine;

namespace Level3
{
    public class FoodMatrixController : MonoBehaviour
    {
        public FoodMatrix foodMatrix;

        private void InitializeFoodMatrix()
        {
            var textAsset = (TextAsset)Resources.Load("matrix");
            foodMatrix = JsonUtility.FromJson<FoodMatrix>(textAsset.text);
        }
        
        private void Start()
        {
            InitializeFoodMatrix();
        }
    }
}