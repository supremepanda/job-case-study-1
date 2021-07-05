using UnityEngine;

namespace Level3
{
    public enum MatrixType
    {
        Default,
        LevelEditor,
        ImageGenerator
    }
    
    public class FoodMatrixController : MonoBehaviour
    {
        [SerializeField] private MatrixType matrixType;
        
        private int[,] _gameArea;
        
        private FoodMatrix _foodMatrix;
        private FoodSpawner _foodSpawner;

        /// <summary>
        /// Convert 1D array to 2D array
        /// </summary>
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
        
        /// <summary>
        /// Initialize food matrix according to selected matrixType. 
        /// </summary>
        private void InitializeFoodMatrix()
        {
            TextAsset textAsset = null;
            if (matrixType == MatrixType.Default)
            {
                textAsset = (TextAsset)Resources.Load("matrix");
            }
            else if (matrixType == MatrixType.LevelEditor)
            {
                textAsset = (TextAsset)Resources.Load("levelEditorMatrix");
            }
            else if (matrixType == MatrixType.ImageGenerator)
            {
                textAsset = (TextAsset)Resources.Load("imageGeneratorMatrix");
            }
            
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