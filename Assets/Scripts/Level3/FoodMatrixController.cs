using System;
using UnityEngine;

namespace Level3
{
    public class FoodMatrixController : MonoBehaviour
    {
        private int[,] _gameArea;
        
        private FoodMatrix _foodMatrix;
        private FoodSpawner _foodSpawner;
        
        private static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }
        
        private void InitializeFoodMatrix()
        {
            var textAsset = (TextAsset)Resources.Load("matrix");
            _foodMatrix = JsonUtility.FromJson<FoodMatrix>(textAsset.text);

            _gameArea = Make2DArray(_foodMatrix.matrix, 6, 5);

        }
        
        private void Start()
        {
            _foodSpawner = FindObjectOfType<FoodSpawner>();
            InitializeFoodMatrix();
            _foodSpawner.SpawnFoods(_gameArea);
        }
    }
}